using System;

namespace SmartLibrary.Models
{
    public enum UserRole
    {
        Administrator,
        Clan
    }

    public class User
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public UserRole Uloga { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Ime} {Prezime} - {Email} - Uloga: {Uloga}";
        }
    }
}
