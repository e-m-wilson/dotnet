# Identity Demo Notes

## Demo Order

0. Add appsettings.json to gitignore
1. Run ```git update-index --skip-worktree ECommerceDemo.API/appsettings.json``` to remove appsettings.json from git but not local file system
2. Add packages
3. Make all changes EXCEPT seeding
4. Create Migration, Apply Migration
5. Database Update
6. Apply role seeding

## New packages

### ECommerceDemo.API proj

```Bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### ECommerceDemo.Data proj

```Bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

### ECommerceDemo.Services proj

```Bash
dotnet add package Microsoft.Extensions.Configuration.Binder
```

### ECommerceDemo.Models proj
```Bash
dotnet add package Microsoft.AspNetCore.Identity
```


## Add to appsettings.json

```JSON
"Jwt": {
    "Key": "DBBFBD9CCB5CC1948D64B4F6835E4234234234",
    "Issuer": "localhost",
    "Audience": "*",
    "ExpireDays": 7
  }
```

## Program.cs Changes

### New Using statement area

```Csharp
using System.Text;
using ECommerceDemo.Data;
using ECommerceDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
```

### Add to the builder services area

```Csharp
// Add Identity
builder
    .Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>();

// Add JWT Authentication
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Add Authorization
builder.Services.AddAuthorization();
```

### Modify AddSwaggerGen to support auth

```Csharp
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        }
    );
});
```

### Make Sure this is present before MapControllers

This adds some seeded services calling our RoleInitializer. Comment this out after initial seed.

```Csharp
app.UseAuthorization(); //Uses Authorization middleware

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await RolesInitializer.SeedRoles(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error seeding roles");
    }
}
```

## Create RoleInitializer

This will run once and initialize our Roles

RoleInitializer.cs

```Csharp
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceDemo.Services;

public static class RolesInitializer
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
```

## Services layer changes

### IAuthService.cs

```Csharp
namespace ECommerceDemo.Services;

public interface IAuthService
{
   Task<string> GenerateToken(ApplicationUser user);
}
```

### AuthService.cs

```Csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceDemo.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(_config.GetValue<double>("Jwt:ExpireDays")),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

## Models layer changes

### AppUser.cs - Model

```Csharp
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Add custom properties if needed - Many properties such as email, username and
    // password come in automatically from IdentityUser
    public string? FullName { get; set; }
}
```

### RegisterDto.cs + LoginDto.cs

LoginDto.cs

```Csharp
using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Models;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
```

RegisterDto.cs

```Csharp
using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Models;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? FullName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
```

## Data Layer Changes

### AppDbContext.cs

```Csharp
using ECommerceDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ECommerceDemo.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();

}
```

## Controller Changes

### AuthController.cs

```Csharp
using ECommerceDemo.Models;
using ECommerceDemo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceDemo.API;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = new ApplicationUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FullName = registerDto.FullName
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(new { message = "Registration successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized("Invalid credentials");
        }
        var token = await _authService.GenerateToken(user);
        return Ok(new { token });
    }
}
```

### ProductController.cs

Add this tag above ApiController tag

```Csharp
[Authorize]
```
{
  "email": "ellie@example.com",
  "fullName": "Ellie",
  "password": "Ellie123"
}