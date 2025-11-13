using System;
using SmartLibrary.Models;
using SmartLibrary.Data;

namespace SmartLibrary.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public void RegistrujKorisnika()
        {
            Console.Write("Unesite ime: ");
            string ime = Console.ReadLine();

            Console.Write("Unesite prezime: ");
            string prezime = Console.ReadLine();

            Console.Write("Unesite email: ");
            string email = Console.ReadLine();

            if (!email.Contains("@"))
            {
                Console.WriteLine("❌ Email mora sadržavati '@'.");
                return;
            }

            Console.Write("Odaberite ulogu (1 - Administrator, 2 - Član): ");
            int izbor = int.Parse(Console.ReadLine());
            var uloga = izbor == 1 ? UserRole.Administrator : UserRole.Clan;

            var user = new User
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Uloga = uloga
            };

            _repo.Add(user);
            Console.WriteLine("✅ Korisnik uspješno registrovan!");
        }

        public void AzurirajKorisnika()
        {
            Console.Write("Unesite ID korisnika za ažuriranje: ");
            int id = int.Parse(Console.ReadLine());
            var user = _repo.GetById(id);
            if (user == null)
            {
                Console.WriteLine("❌ Korisnik nije pronađen!");
                return;
            }

            Console.Write("Novo ime: ");
            user.Ime = Console.ReadLine();

            Console.Write("Novo prezime: ");
            user.Prezime = Console.ReadLine();

            Console.Write("Novi email: ");
            user.Email = Console.ReadLine();

            _repo.Update(user);
            Console.WriteLine("✅ Korisnik ažuriran!");
        }

        public void ObrisiKorisnika()
        {
            Console.Write("Unesite ID korisnika za brisanje: ");
            int id = int.Parse(Console.ReadLine());
            if (_repo.Delete(id))
                Console.WriteLine("✅ Korisnik obrisan!");
            else
                Console.WriteLine("❌ Korisnik nije pronađen!");
        }

        public void PrikaziSve()
        {
            var users = _repo.GetAll();
            if (users.Count == 0)
            {
                Console.WriteLine("Nema registrovanih korisnika.");
                return;
            }

            Console.WriteLine("\n📋 Spisak korisnika:");
            foreach (var u in users)
                Console.WriteLine(u);
        }
    }
}
