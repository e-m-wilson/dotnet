using ECommerceDemo.Models;
using ECommerceDemo.Services;
using Microsoft.AspNetCore.Authorization;
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

    //[Authorize(Roles = "Admin")]
    [HttpPost("registeradmin")]
    public async Task<IActionResult> Register(RegisterDto newUser)
    {
        //If the LoginDto doesnt conform to our model binding rules, just kick it back
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //Using the DTO's info to construct a new ApplicationUser model object to stick
        //into the database
        var user = new ApplicationUser
        {
            UserName = newUser.Email,
            Email = newUser.Email,
            FullName = newUser.FullName
        };

        //We are going to attempt to add the user to the db, if they are added we will return
        //an Ok with a success message
        //If not, we return some error
        var result = await _userManager.CreateAsync(user, newUser.Password);

        // result above is of type IdentityResult
        // It contains info about an AspNetCore.Identity related database operation
        //In this case, did we succeed in creating a new user record on our database
        //If not, we run the code below. If we did succeed, we just return the Ok with a message.
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, "Admin");

        return Ok(new { message = "Registration Successful" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto existingUser)
    {
        //If the LoginDto doesnt conform to our model binding rules, just kick it back
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        //Reach into the db and find the ApplicationUser with this email
        var user = await _userManager.FindByEmailAsync(existingUser.Email);

        //If we do not succeed...
        if (user == null || !await _userManager.CheckPasswordAsync(user, existingUser.Password))
        {
            return Unauthorized("invalid Credentials");
        }

        var token = await _authService.GenerateToken(user);

        return Ok(new { token });
    }
}
