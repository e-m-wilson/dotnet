# ASP.NET Core Identity + JWT Authentication Complete Guide

## 📦 Packages

```bash
# Core Packages
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.Extensions.Configuration.Binder
System.IdentityModel.Tokens.Jwt
```

## 🏗️ Architecture

```Text
API Layer      → Controllers, Auth Setup
Service Layer  → AuthService, Token Generation
Models Layer   → ApplicationUser, DTOs
Repository     → DbContext, Migrations
```

## 🔧 Program.cs Configuration

```csharp
// ===== Identity Setup =====
builder.Services.AddIdentityCore<ApplicationUser>(options => {
   options.Password.RequireDigit = true;
   options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()              // Enable roles
.AddEntityFrameworkStores<AppDbContext>()  // Connect to DB
.AddSignInManager<SignInManager<ApplicationUser>>();
// ===== JWT Configuration =====
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
   options.TokenValidationParameters = new TokenValidationParameters {
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
// ===== Middleware Order =====
app.UseAuthentication();
app.UseAuthorization();
```

## 👤 Identity Classes

```csharp
// Models/ApplicationUser.cs
public class ApplicationUser : IdentityUser {
   public string FullName { get; set; }  // Custom property
}
// Repository/AppDbContext.cs
public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string> {
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
```

## 🔐 AuthService Deep Dive

```csharp
public class AuthService : IAuthService {
   private readonly UserManager<ApplicationUser> _userManager;
   private readonly IConfiguration _config;
   public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config) {
       _userManager = userManager;
       _config = config;
   }
   public async Task<string> GenerateToken(ApplicationUser user) {
       // 1. Base Claims
       var claims = new List<Claim> {
           new Claim(JwtRegisteredClaimNames.Sub, user.Id),
           new Claim(JwtRegisteredClaimNames.Email, user.Email),
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
       };
       // 2. Role Claims
       var roles = await _userManager.GetRolesAsync(user);
       claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
       // 3. Security Components
       var key = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(_config["Jwt:Key"])
       );
       var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
       // 4. Token Assembly
       var token = new JwtSecurityToken(
           issuer: _config["Jwt:Issuer"],
           audience: _config["Jwt:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddHours(
               _config.GetValue<double>("Jwt:ExpireHours")
           ),
           signingCredentials: creds
       );
       // 5. Token Serialization
       return new JwtSecurityTokenHandler().WriteToken(token);
   }
}
```

### AuthService Method Breakdown

| Method/Class | Purpose | Namespace |
|--------------|---------|-----------|
| `GetRolesAsync()` | Get user's roles | `Microsoft.AspNetCore.Identity` |
| `JwtSecurityToken` | Token construction | `System.IdentityModel.Tokens.Jwt` |
| `SigningCredentials` | Cryptographic signing | `Microsoft.IdentityModel.Tokens` |
| `WriteToken()` | Serialize to JWT string | `System.IdentityModel.Tokens.Jwt` |

## 🚪 Auth Controller Endpoints

```csharp
[HttpPost("register")]
public async Task<IActionResult> Register(RegisterDto dto) {
   var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
   var result = await _userManager.CreateAsync(user, dto.Password);
   if (!result.Succeeded) return BadRequest(result.Errors);
   return Ok(new { message = "Registration successful" });
}
[HttpPost("login")]
public async Task<IActionResult> Login(LoginDto dto) {
   var user = await _userManager.FindByEmailAsync(dto.Email);
   if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
       return Unauthorized("Invalid credentials");
   var token = await _authService.GenerateToken(user);
   return Ok(new { token });
}
```

## 📋 DTOs

```csharp
public class RegisterDto {
   [Required][EmailAddress] public string Email { get; set; }
   [Required][StringLength(100)] public string FullName { get; set; }
   [Required][DataType(DataType.Password)] public string Password { get; set; }
}
public class LoginDto {
   [Required][EmailAddress] public string Email { get; set; }
   [Required][DataType(DataType.Password)] public string Password { get; set; }
}
```

## 🔑 Key Method Calls

| Method | Class/Service | Purpose |
|--------|---------------|---------|
| `CreateAsync()` | UserManager | User creation |
| `FindByEmailAsync()` | UserManager | User lookup |
| `CheckPasswordAsync()` | UserManager | Password validation |
| `GenerateToken()` | AuthService | JWT generation |
| `AddAuthentication()` | Program.cs | Auth middleware setup |
| `AddAuthorization()` | Program.cs | Policy setup |

## 🗄️ Database Operations

```bash
# Migration Commands
dotnet ef migrations add "AddIdentity"
dotnet ef database update
# Key Tables
- AspNetUsers (Extended via ApplicationUser)
- AspNetRoles
- AspNetUserRoles
- AspNetUserClaims
- AspNetUserLogins
```

## 🛡️ Authorization Usage

```csharp
[Authorize]  // Any authenticated user
[Authorize(Roles = "Admin")]  // Role-based
[Authorize(Policy = "CustomPolicy")]  // Policy-based
[AllowAnonymous]  // Bypass auth
```

## 🔗 JWT Flow

1. Client → `POST /api/auth/login` with credentials
2. Server → Validate credentials → Generate JWT
3. Client → Store JWT → Include in `Authorization: Bearer <token>` header
4. Server → Validate JWT on each request → Grant access

## 📂 File Structure

```
/API
 ├─ Controllers
 │  ├─ AuthController.cs
 │  └─ SecureController.cs
/Services
 ├─ IAuthService.cs
 └─ AuthService.cs
/Models
 ├─ ApplicationUser.cs
 └─ DTOs
    ├─ RegisterDto.cs
    └─ LoginDto.cs
/Repository
 ├─ AppDbContext.cs
 └─ Migrations/
```

## 🚨 Common Errors & Solutions

| Error | Solution |
|-------|----------|
| "Store does not implement IUserRoleStore" | Add `.AddRoles<IdentityRole>()` in Program.cs |
| JWT validation fails | Verify issuer/audience match in appsettings.json |
| Roles not showing in token | Ensure user has roles in AspNetUserRoles table |
| "Invalid token" | Check token expiration and server clock sync |

## ⚙️ appsettings.json

```json
"ConnectionStrings": {
 "DefaultConnection": "Server=...;Database=..."
},
"Jwt": {
 "Key": "base64-256-bit-secret",
 "Issuer": "https://your-api.com",
 "Audience": "https://your-client.com",
 "ExpireHours": 24
}
