namespace SmartLibrary.Models;

public class BorrowHistory
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime DateBorrowed { get; set; }

    public BorrowHistory(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
        DateBorrowed = DateTime.Now;
    }
}
