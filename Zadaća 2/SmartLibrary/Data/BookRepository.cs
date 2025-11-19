using SmartLibrary.Inventory;

namespace SmartLibrary.Data;

public class BookRepository
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public List<Book> GetBooksByGenre(string genre)
    {
        return books.Where(b => b.Genre == genre).ToList();
    }
}
