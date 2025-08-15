using Microsoft.EntityFrameworkCore;
using SchoolDemo.Models;

namespace SchoolDemo.Data;

public class SchoolDbContext : DbContext
{
    //Your DbContext needs a constructor - this constructor takes a special argument of type
    //DbContextOptions - we need to call the base (parent) constructor since it is the one
    // who uses the options argument
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options) { }

    //We need to add our entities, that EF will track for us
    //We do that by creating DbSet objects
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }

    //90% of the time, we could get away without the following. But in our case,
    //we will use it to seed data as well as show off fluent api
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // //Seeding Instructors
        // modelBuilder
        //     .Entity<Instructor>()
        //     .HasData(
        //         new Instructor
        //         {
        //             Id = 1,
        //             FirstName = "Jon",
        //             LastName = "Doe"
        //         },
        //         new Instructor
        //         {
        //             Id = 2,
        //             FirstName = "Jane",
        //             LastName = "Doe"
        //         }
        //     );

        // Lets try to configure a many to many using fluent API
        // The documentation cautions you against this, but we cant read.

        //modelBuilder.Entity<Student>().HasMany(e => e.Courses).WithMany(e => e.Students);
    }
}
