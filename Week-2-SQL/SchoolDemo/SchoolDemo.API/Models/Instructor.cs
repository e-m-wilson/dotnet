using System.ComponentModel.DataAnnotations;

namespace SchoolDemo.Models;

public class Instructor
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    //Relationship stuff goes here
    public List<Course> Courses { get; set; } = new List<Course>(); //Remember to initialize your lists!
}
