using System.Collections.Generic;
using System.Linq;
using SmartLibrary.Models;

namespace SmartLibrary.Data
{
    public class UserRepository
    {
        private List<User> _users = new List<User>();
        private int _nextId = 1;

        public List<User> GetAll() => _users;

        public User GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public bool Update(User user)
        {
            var existing = GetById(user.Id);
            if (existing == null) return false;

            existing.Ime = user.Ime;
            existing.Prezime = user.Prezime;
            existing.Email = user.Email;
            existing.Uloga = user.Uloga;
            return true;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }
    }
}
