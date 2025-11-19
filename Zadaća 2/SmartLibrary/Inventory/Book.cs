namespace SmartLibrary.Inventory
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Genre { get; set; } = "";
        
        // Koristimo IsAvailable jer ga LibraryInventory i BorrowService očekuju
        public bool IsAvailable { get; set; } = true;

        // Konstruktor bez parametara — potreban zbog LibraryInventory
        public Book() {}

        // Konstruktor za osnovne informacije (koristi se u RecommendationService)
        public Book(int id, string title, string genre)
        {
            Id = id;
            Title = title;
            Genre = genre;
            Author = "";
        }

        // Konstruktor za sve podatke o knjizi
        public Book(int id, string title, string author, string genre, bool isAvailable = true)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            IsAvailable = isAvailable;
        }

        public override string ToString()
            => $"{Id}. {Title} - {Author} ({Genre}) | {(IsAvailable ? "Dostupna" : "Nedostupna")}";
    }
}
