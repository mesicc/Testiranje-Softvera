using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartLibrary
{
    public class Library
    {
        private readonly HashSet<string> _users = new HashSet<string>();
        private readonly List<string> _books = new List<string>();

        private readonly Dictionary<string, List<string>> _loans =
            new Dictionary<string, List<string>>();

        private readonly Dictionary<string, string> _reservations =
            new Dictionary<string, string>();

        public int UserCount => _users.Count;

        public void AddUser(string name)
        {
            _users.Add(name);

            if (!_loans.ContainsKey(name))
                _loans[name] = new List<string>();
        }

        public void RemoveUser(string name)
        {
            _users.Remove(name);
            _loans.Remove(name);
        }

        public void AddBook(string title)
        {
            if (!_books.Contains(title))
                _books.Add(title);
        }

        public void RemoveBook(string title)
        {
            _books.Remove(title);
            _reservations.Remove(title);

            foreach (var user in _loans.Keys)
                _loans[user].Remove(title);
        }

        public List<string> GetAllBooks()
        {
            return new List<string>(_books);
        }

        public void BorrowBook(string user, string title)
        {
            if (!_users.Contains(user))
                throw new InvalidOperationException("User not found.");

            if (!_books.Contains(title))
                throw new InvalidOperationException("Book not found.");

            if (_reservations.TryGetValue(title, out string reservedBy))
            {
                if (reservedBy != user)
                    throw new InvalidOperationException("Book is reserved by another user.");
            }

            _loans[user].Add(title);
        }

        public void ReturnBook(string user, string title)
        {
            if (_loans.ContainsKey(user))
                _loans[user].Remove(title);
        }

        public List<string> GetUserLoans(string user)
        {
            if (_loans.ContainsKey(user))
                return new List<string>(_loans[user]);

            return new List<string>();
        }

        public List<string> RecommendBooks(string user)
        {
            var borrowed = GetUserLoans(user);
            return _books.Where(b => !borrowed.Contains(b)).ToList();
        }

        public void ReserveBook(string title, string user)
        {
            _reservations[title] = user;
        }

        public bool IsBookReserved(string title)
        {
            return _reservations.ContainsKey(title);
        }
    }
}
