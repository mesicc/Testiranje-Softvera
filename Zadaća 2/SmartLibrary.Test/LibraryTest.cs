using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SmartLibrary;
using System.Collections.Generic;
using SmartLibrary.Models;
using SmartLibrary.Services;

namespace SmartLibrary.Tests
{
    [TestClass]
    public class LibraryTests
    {
        // Test 1: Dodavanje korisnika
        [TestMethod]
        public void AddUser_ShouldIncreaseUserCount()
        {
            var library = new Library();
            library.AddUser("Kemal");
            Assert.AreEqual(1, library.UserCount);
        }

        // Test 2: Uklanjanje korisnika
        [TestMethod]
        public void RemoveUser_ShouldDecreaseUserCount()
        {
            var library = new Library();
            library.AddUser("Kemal");
            library.RemoveUser("Kemal");
            Assert.AreEqual(0, library.UserCount);
        }

        // Test 3: Dodavanje knjige
        [TestMethod]
        public void AddBook_ShouldAddBookToInventory()
        {
            var library = new Library();
            library.AddBook("C# Basics");

            var books = library.GetAllBooks();
            CollectionAssert.Contains(books, "C# Basics");
        }

        // Test 4: Uklanjanje knjige
        [TestMethod]
        public void RemoveBook_ShouldRemoveBookFromInventory()
        {
            var library = new Library();
            library.AddBook("C# Basics");
            library.RemoveBook("C# Basics");

            var books = library.GetAllBooks();
            CollectionAssert.DoesNotContain(books, "C# Basics");
        }

        // Test 5: Posudba knjige
        [TestMethod]
        public void BorrowBook_ShouldAddToUserLoans()
        {
            var library = new Library();
            library.AddUser("Kemal");
            library.AddBook("C# Basics");

            library.BorrowBook("Kemal", "C# Basics");

            var loans = library.GetUserLoans("Kemal");
            CollectionAssert.Contains(loans, "C# Basics");
        }

        // Test 6: Vraćanje knjige
        [TestMethod]
        public void ReturnBook_ShouldRemoveFromUserLoans()
        {
            var library = new Library();
            library.AddUser("Kemal");
            library.AddBook("C# Basics");

            library.BorrowBook("Kemal", "C# Basics");
            library.ReturnBook("Kemal", "C# Basics");

            var loans = library.GetUserLoans("Kemal");
            CollectionAssert.DoesNotContain(loans, "C# Basics");
        }

        // Test 7: Preporuke knjiga
        [TestMethod]
        public void RecommendBooks_ShouldReturnList()
        {
            var library = new Library();

            library.AddUser("Kemal");
            library.AddBook("C# Basics");
            library.AddBook("Advanced C#");

            library.BorrowBook("Kemal", "C# Basics");

            var recommendations = library.RecommendBooks("Kemal");

            CollectionAssert.Contains(recommendations, "Advanced C#");
        }

        // Test 8: Rezervacija knjige
        [TestMethod]
        public void ReserveBook_ShouldMarkBookAsReserved()
        {
            var library = new Library();
            library.AddBook("C# Basics");

            library.ReserveBook("C# Basics", "Kemal");

            Assert.IsTrue(library.IsBookReserved("C# Basics"));
        }

        // Test 9: Data-driven test
        [DataTestMethod]
        [DataRow("Book1")]
        [DataRow("Book2")]
        [DataRow("Book3")]
        public void AddMultipleBooks_ShouldAddEachBook(string bookTitle)
        {
            var library = new Library();
            library.AddBook(bookTitle);

            var books = library.GetAllBooks();
            CollectionAssert.Contains(books, bookTitle);
        }

        // Test 10: Posudba rezervirane knjige
        [TestMethod]
        public void BorrowReservedBook_ShouldThrowException()
        {
            var library = new Library();

            library.AddUser("Kemal");
            library.AddUser("Amra");

            library.AddBook("C# Basics");
            library.ReserveBook("C# Basics", "Kemal");

           var ex = Assert.ThrowsException<InvalidOperationException>(() =>
    library.BorrowBook("Amra", "C# Basics")
);

            Assert.AreEqual("Book is reserved by another user.", ex.Message);
        }
    }
}