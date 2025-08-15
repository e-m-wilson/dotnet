using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SchoolDemo.Models;

public class Student
{
    public int Id { get; set; }

    [Required, MaxLength(40)]
    public string FirstName { get; set; }

    [Required, MaxLength(40)]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    //We'll come back and configure relationship stuff here
    public List<Course> Courses { get; set; } = new();
}
