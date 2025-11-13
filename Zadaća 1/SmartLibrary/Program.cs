using SmartLibrary.Data;
using SmartLibrary.Models;
using SmartLibrary.Services;
using System;

class Program
{
    static void Main()
    {
        var repo = new UserRepository();
        var service = new UserService(repo);

        while (true)
        {
            Console.WriteLine("\n--- SmartLibrary: Upravljanje korisnicima ---");
            Console.WriteLine("1. Registruj korisnika");
            Console.WriteLine("2. Prikaži sve korisnike");
            Console.WriteLine("3. Ažuriraj korisnika");
            Console.WriteLine("4. Obriši korisnika");
            Console.WriteLine("5. Izlaz");
            Console.Write("Izbor: ");
            var izbor = Console.ReadLine();

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
                        Console.Write("Unesite ID korisnika: ");
                        var id = int.Parse(Console.ReadLine());
                        var user = repo.GetById(id);
                        if (user == null) { Console.WriteLine("Korisnik nije pronađen."); break; }

                        Console.Write("Novo ime: ");
                        user.Ime = Console.ReadLine();
                        service.AzurirajKorisnika(user);
                        Console.WriteLine("Korisnik ažuriran!");
                        break;

                    case "4":
                        Console.Write("Unesite ID korisnika za brisanje: ");
                        service.ObrisiKorisnika(int.Parse(Console.ReadLine()));
                        Console.WriteLine("Korisnik obrisan!");
                        break;

                    case "5":
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }
        }
    }
}
