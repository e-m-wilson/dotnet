using ECommerceDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceDemo.Data;

//EF Core integrates with ASP.Net Identity out of the box, we can inherit from
//IdentityDbContext to gain access to these features.
//IdentityDbContext can be configured to use our ApplicationUser (extends IdentityUser)
//with no additional config as long as we type it using <>. We need a user (our user class)
// a role (we used the prebuilt IdentityRole), and a key. In our case a string.
public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}
