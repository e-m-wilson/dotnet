using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceDemo.Services;

public class AuthService : IAuthService
{
    //My AuthService needs a few objects that will be injected into it

    //Think of this as AuthService's DbContext
    // UserManager has a bunch of prebuilt methods for Db operations related to user Authentication and Authorization
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    //Like any service, we need a constructor so that the DI container can inject the dependencies above at runtime for us
    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    //This method is what will generate that JWT token when a user logs in
    //This is the primary (in our case, only) responsibility of AuthService
    public async Task<string> GenerateToken(ApplicationUser user)
    {
        //Claims are assertions about our users. Below we are "claiming" that a user has:
        // A username, an Id, and an Email. There are many more types of claims, use what you need
        // and only what you need
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };

        // Roles are stored in our database just like our users and our tokens
        // UserManager has prebuilt methods for reaching into our database and reading these tables
        // Here we grab a list of potential roles for a user
        var roles = await _userManager.GetRolesAsync(user);

        //Whatever role our user is, we add that to our list of claims
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        //Now we will begin engaging with the cryptography library to create our JWTs.
        //We will pull things out of our config to do so, remember that our _config object
        //pulled these bits of info from appsettings.json
        var jwtKey =
            _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

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
