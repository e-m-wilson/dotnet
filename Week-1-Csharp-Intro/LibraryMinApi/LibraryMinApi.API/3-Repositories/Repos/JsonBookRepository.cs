using System.Text.Json;
using Library.Models;

namespace Library.Repositories;

public class JsonBookRepository : IBookRepository
{
    private readonly string _filePath;

    public JsonBookRepository()
    {
        _filePath = Path.Combine("./5-Data-Files/books.json");
    }

    public List<Book> GetAllBooks()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<Book>();

            using var stream = File.OpenRead(_filePath);

            return JsonSerializer.Deserialize<List<Book>>(stream) ?? new List<Book>();
        }
        catch
        {
            throw new Exception("Failed to retrieve books");
        }
    }

    // Create operation with duplicate check
    public Book AddBook(Book book)
    {
        var books = GetAllBooks();
        if (books.Any(b => b.Isbn == book.Isbn))
            throw new Exception("Book with same ID already exists");
        books.Add(book);
        SaveBooks(books);
        return book;
    }

    // Update operation with error handling
    public Book UpdateBook(Book book)
    {
        var books = GetAllBooks();

        var index = books.FindIndex(b => b.Isbn == book.Isbn);

        if (index == -1)
            throw new Exception("Book not found");

        books[index] = book;

        SaveBooks(books);

        return book;
    }

    public void SaveBooks(List<Book> members)
    {
        using var stream = File.Create(_filePath); //Creating the file
        JsonSerializer.Serialize(stream, members); //Our file will hold a list of Books, serialized to Json
    }
}
