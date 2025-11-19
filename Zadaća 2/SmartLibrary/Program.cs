using System;
using SmartLibrary.Data;
using SmartLibrary.Services;
using SmartLibrary.Models;
using SmartLibrary.Inventory;

class Program
{
    static void Main()
    {
        var inventory = new LibraryInventory();
        var userRepo = new UserRepository();
        var userService = new UserService(userRepo);
        var borrowRepo = new BorrowRepository();
        var borrowService = new BorrowService(borrowRepo, inventory, userRepo);

        // Repozitoriji za preporuke  
        var bookRepo = new BookRepository();
        var historyRepo = new BorrowHistoryRepository();
        var recService = new RecommendationService(bookRepo, historyRepo);

        // --- Dodavanje knjiga u BookRepo ---
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(1, "Na Drini ćuprija", "Ivo Andrić", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(2, "Travnička hronika", "Ivo Andrić", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(3, "Prokleta avlija", "Ivo Andrić", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(4, "Derviš i smrt", "Meša Selimović", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(5, "Tvrđava", "Meša Selimović", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(6, "Bašta, pepeo", "Danilo Kiš", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(7, "Rani jadi", "Danilo Kiš", "Roman"));
        bookRepo.AddBook(new SmartLibrary.Inventory.Book(8, "Hazarski rječnik", "Milorad Pavić", "Eksperimentalni roman"));

        // Historija korisnika  
        historyRepo.AddRecord(new BorrowHistory(1, 1));
        historyRepo.AddRecord(new BorrowHistory(1, 3));

        while (true)
        {
            Console.WriteLine("\n=== SMART LIBRARY SISTEM ===");
            Console.WriteLine("1. Upravljanje korisnicima");
            Console.WriteLine("2. Inventar knjiga");
            Console.WriteLine("3. Sistem posudbe");
            Console.WriteLine("4. Preporuke za korisnika");
            Console.WriteLine("0. Izlaz");
            Console.Write("Izbor: ");

            var main = Console.ReadLine();
            if (main == "0") return;

            switch (main)
            {
                case "1":
                    UserMenu(userService, userRepo);
                    break;

                case "2":
                    BookMenu(inventory);
                    break;

                case "3":
                    BorrowMenu(borrowService);
                    break;

                case "4":
                    Console.Write("ID korisnika: ");
                    int uid = int.Parse(Console.ReadLine());
                    var rec = recService.GetRecommendations(uid);

                    Console.WriteLine("\n--- Preporučene knjige ---");
                    foreach (var b in rec)
                        Console.WriteLine($"{b.Title} - {b.Author}");
                    break;

                default:
                    Console.WriteLine("Pogrešan izbor.");
                    break;
            }
        }
    }

    // --- USER MENU ---
    static void UserMenu(UserService service, UserRepository repo)
    {
        while (true)
        {
            Console.WriteLine("\n--- Upravljanje korisnicima ---");
            Console.WriteLine("1. Registruj korisnika");
            Console.WriteLine("2. Prikaži sve korisnike");
            Console.WriteLine("3. Ažuriraj korisnika");
            Console.WriteLine("4. Obriši korisnika");
            Console.WriteLine("0. Nazad");
            Console.Write("Izbor: ");

            var izbor = Console.ReadLine();
            if (izbor == "0") return;

            try
            {
                switch (izbor)
                {
                    case "1":
                        Console.Write("Ime: ");
                        var ime = Console.ReadLine();
                        Console.Write("Prezime: ");
                        var prezime = Console.ReadLine();
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        Console.Write("Lozinka: ");
                        var lozinka = Console.ReadLine();
                        Console.Write("Uloga (Administrator/Clan): ");
                        var uloga = Enum.Parse<UserRole>(Console.ReadLine(), true);

                        service.RegistrujKorisnika(ime, prezime, email, lozinka, uloga);
                        Console.WriteLine("Korisnik uspješno registrovan!");
                        break;

                    case "2":
                        service.PrikaziSve();
                        break;

                    case "3":
                        Console.Write("ID korisnika: ");
                        int id = int.Parse(Console.ReadLine());
                        var user = repo.GetById(id);

                        if (user == null)
                        {
                            Console.WriteLine("Korisnik nije pronađen.");
                            break;
                        }

                        Console.Write("Novo ime: ");
                        user.Ime = Console.ReadLine();
                        service.AzurirajKorisnika(user);
                        Console.WriteLine("Korisnik ažuriran!");
                        break;

                    case "4":
                        Console.Write("ID korisnika: ");
                        service.ObrisiKorisnika(int.Parse(Console.ReadLine()));
                        Console.WriteLine("Korisnik obrisan!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška: " + ex.Message);
            }
        }
    }

    // --- BOOK MENU ---
    static void BookMenu(LibraryInventory inventory)
    {
        while (true)
        {
            Console.WriteLine("\n--- Inventar knjiga ---");
            Console.WriteLine("1. Dodaj knjigu");
            Console.WriteLine("2. Ažuriraj knjigu");
            Console.WriteLine("3. Obriši knjigu");
            Console.WriteLine("4. Pretraga");
            Console.WriteLine("5. Prikaži sve knjige");
            Console.WriteLine("6. Promijeni dostupnost");
            Console.WriteLine("0. Nazad");
            Console.Write("Izbor: ");

            string opcija = Console.ReadLine();
            if (opcija == "0") return;

            switch (opcija)
            {
                case "1":
                    Console.Write("Naslov: ");
                    string title = Console.ReadLine();
                    Console.Write("Autor: ");
                    string author = Console.ReadLine();
                    Console.Write("Žanr: ");
                    string genre = Console.ReadLine();

                    inventory.AddBook(title, author, genre);
                    Console.WriteLine("Knjiga dodana!");
                    break;

                case "2":
                    Console.Write("ID knjige: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Novi naslov: ");
                    string newTitle = Console.ReadLine();
                    Console.Write("Novi autor: ");
                    string newAuthor = Console.ReadLine();
                    Console.Write("Novi žanr: ");
                    string newGenre = Console.ReadLine();

                    if (inventory.UpdateBook(updateId, newTitle, newAuthor, newGenre))
                        Console.WriteLine("Knjiga ažurirana!");
                    else
                        Console.WriteLine("Knjiga nije pronađena.");
                    break;

                case "3":
                    Console.Write("ID: ");
                    int deleteId = int.Parse(Console.ReadLine());

                    if (inventory.DeleteBook(deleteId))
                        Console.WriteLine("Knjiga obrisana!");
                    else
                        Console.WriteLine("Greška – knjiga ne postoji.");
                    break;

                case "4":
                    Console.Write("Pretraga: ");
                    var q = Console.ReadLine();
                    inventory.Search(q).ForEach(Console.WriteLine);
                    break;

                case "5":
                    inventory.GetAllBooks().ForEach(Console.WriteLine);
                    break;

                case "6":
                    Console.Write("ID knjige: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Dostupna (1) / Nedostupna (0): ");
                    bool available = Console.ReadLine() == "1";

                    if (inventory.SetAvailability(id, available))
                        Console.WriteLine("Status promijenjen!");
                    else
                        Console.WriteLine("Knjiga nije pronađena.");
                    break;
            }
        }
    }

    // --- BORROW MENU ---
    static void BorrowMenu(BorrowService service)
    {
        while (true)
        {
            Console.WriteLine("\n--- Sistem posudbe ---");
            Console.WriteLine("1. Posudi knjigu");
            Console.WriteLine("2. Vrati knjigu");
            Console.WriteLine("3. Aktivne posudbe");
            Console.WriteLine("4. Historija posudbi");
            Console.WriteLine("0. Nazad");
            Console.Write("Izbor: ");

            string opcija = Console.ReadLine();
            if (opcija == "0") return;

            try
            {
                switch (opcija)
                {
                    case "1":
                        Console.Write("ID korisnika: ");
                        int uid = int.Parse(Console.ReadLine());
                        Console.Write("ID knjige: ");
                        int bid = int.Parse(Console.ReadLine());
                        service.Posudi(uid, bid);
                        Console.WriteLine("Knjiga posuđena!");
                        break;

                    case "2":
                        Console.Write("ID posudbe: ");
                        int pid = int.Parse(Console.ReadLine());
                        service.Vrati(pid);
                        Console.WriteLine("Knjiga vraćena!");
                        break;

                    case "3":
                        service.PrikaziAktivne();
                        break;

                    case "4":
                        service.PrikaziHistoriju();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška: " + ex.Message);
            }
        }
    }
}
