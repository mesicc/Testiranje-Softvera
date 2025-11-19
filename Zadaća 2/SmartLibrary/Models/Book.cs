namespace SmartLibrary.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }

    public BookModel(int id, string title, string genre)
    {
        Id = id;
        Title = title;
        Genre = genre;
    }
}
