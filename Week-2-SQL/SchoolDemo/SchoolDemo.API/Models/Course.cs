using System.ComponentModel.DataAnnotations;

namespace SchoolDemo.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }
    public string Description { get; set; }

    //More relationship stuff will go here
    public Instructor Instructor { get; set; }

    public List<Student> Students { get; set; } = new();
}
