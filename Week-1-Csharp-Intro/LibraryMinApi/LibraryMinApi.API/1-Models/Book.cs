namespace Library.Models;

public class Book
{
    //We will use ISBN for this
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;

    //flag if my book is available
    public bool IsAvailable { get; set; } = true;
}
