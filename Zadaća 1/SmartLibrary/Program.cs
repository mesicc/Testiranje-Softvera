using System;
using SmartLibrary.Services;
using SmartLibrary.Data;

namespace SmartLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new UserRepository();
            var service = new UserService(repo);

            while (true)
            {
                Console.WriteLine("\n===== Sistem za upravljanje korisnicima =====");
                Console.WriteLine("1. Registruj korisnika");
                Console.WriteLine("2. Ažuriraj korisnika");
                Console.WriteLine("3. Obriši korisnika");
                Console.WriteLine("4. Prikaži sve korisnike");
                Console.WriteLine("0. Izlaz");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": service.RegistrujKorisnika(); break;
                    case "2": service.AzurirajKorisnika(); break;
                    case "3": service.ObrisiKorisnika(); break;
                    case "4": service.PrikaziSve(); break;
                    case "0": return;
                    default: Console.WriteLine("❌ Nevažeća opcija."); break;
                }
            }
        }
    }
}
