using System;
using System.Collections.Generic;

namespace SmartLibrary
{
    public class Library
    {
        private readonly List<string> _users = new();
        private readonly List<string> _books = new();
        private readonly Dictionary<string, List<string>> _loans = new();
        private readonly Dictionary<string, string> _reservations = new();

        public int UserCount => _users.Count;

        // === USERS ===
        public void AddUser(string name)
        {
            _users.Add(name);
            _loans[name] = new List<string>();
        }

        public void RemoveUser(string name)
        {
            _users.Remove(name);
            _loans.Remove(name);
        }

        // === BOOKS ===
        public void AddBook(string title)
        {
            _books.Add(title);
        }

        public void RemoveBook(string title)
        {
            _books.Remove(title);
            _reservations.Remove(title);
        }

        public List<string> GetAllBooks()
        {
            return new List<string>(_books);
        }

        // === BORROWING ===
        public void BorrowBook(string user, string book)
        {
            if (_reservations.ContainsKey(book) && _reservations[book] != user)
                throw new InvalidOperationException("Book is reserved by another user.");

            if (!_loans.ContainsKey(user))
                _loans[user] = new List<string>();

            _loans[user].Add(book);
        }

        public void ReturnBook(string user, string book)
        {
            if (_loans.ContainsKey(user))
                _loans[user].Remove(book);
        }

        public List<string> GetUserLoans(string user)
        {
            return _loans.ContainsKey(user)
                ? new List<string>(_loans[user])
                : new List<string>();
        }

        // === RESERVATIONS ===
        public void ReserveBook(string title, string user)
        {
            _reservations[title] = user;
        }

        public bool IsBookReserved(string title)
        {
            return _reservations.ContainsKey(title);
        }

        // === RECOMMENDATIONS ===
        public List<string> RecommendBooks(string user)
        {
            var userLoans = GetUserLoans(user);
            var result = new List<string>();

            foreach (var b in _books)
                if (!userLoans.Contains(b))
                    result.Add(b);

            return result;
        }
    }
}
