using Library.Models;

namespace Library.Repositories;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    Book AddBook(Book book);
    Book UpdateBook(Book book);
    void SaveBooks(List<Book> members);
}
