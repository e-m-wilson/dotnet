namespace Library.Models;

public class Member
{
    public Guid MemberId { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
