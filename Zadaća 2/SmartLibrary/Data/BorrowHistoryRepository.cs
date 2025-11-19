using SmartLibrary.Models;

namespace SmartLibrary.Data;

public class BorrowHistoryRepository
{
    private List<BorrowHistory> history = new List<BorrowHistory>();

    public void AddRecord(BorrowHistory record)
    {
        history.Add(record);
    }

    public List<BorrowHistory> GetUserHistory(int userId)
    {
        return history.Where(h => h.UserId == userId).ToList();
    }
}
