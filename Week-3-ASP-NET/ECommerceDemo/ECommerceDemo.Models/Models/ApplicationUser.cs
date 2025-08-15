using Microsoft.AspNetCore.Identity;

namespace ECommerceDemo.Models;

public class ApplicationUser : IdentityUser
{
    //IdentityUser, which we extend from, already contains many fields we need.
    //Things like username, email, password, etc
    //We can add custom fields *if needed*
    public string? FullName { get; set; }
}
