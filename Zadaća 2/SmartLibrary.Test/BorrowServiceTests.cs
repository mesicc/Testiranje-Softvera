using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Services;
using SmartLibrary.Data;
using SmartLibrary.Inventory;
using SmartLibrary.Models;
using System;
using System.Linq;

namespace SmartLibrary.Tests
{
    [TestClass]
    public class BorrowServiceTests
    {
        private BorrowService CreateService(out UserRepository users, out LibraryInventory inv, out BorrowRepository repo)
        {
            users = new UserRepository();
            inv = new LibraryInventory();
            repo = new BorrowRepository();

            users.Add(new User { Ime = "A", Prezime = "B", Email = "x@x.com", Lozinka = "123", Uloga = UserRole.Clan });
            inv.AddBook("Book", "Autor", "Zanr");

            return new BorrowService(repo, inv, users);
        }

        [TestMethod]
        public void Posudi_ShouldCreateRecord_AndMakeBookUnavailable()
        {
            var svc = CreateService(out var users, out var inv, out var repo);

            svc.Posudi(1, 1);

            Assert.AreEqual(1, repo.GetHistory().Count());
            Assert.IsFalse(inv.GetAllBooks().First().IsAvailable);
        }

        [TestMethod]
        public void Posudi_ShouldThrow_WhenUserNotFound()
        {
            var users = new UserRepository();
            var inv = new LibraryInventory();
            var repo = new BorrowRepository();

            inv.AddBook("Book", "Autor", "Zanr");

            var svc = new BorrowService(repo, inv, users);

            Assert.ThrowsException<Exception>(() => svc.Posudi(1, 1));
        }

        [TestMethod]
        public void Vrati_ShouldMarkAsReturned_AndMakeBookAvailable()
        {
            var svc = CreateService(out var users, out var inv, out var repo);

            svc.Posudi(1, 1);
            var rec = repo.GetHistory().First();

            svc.Vrati(rec.Id);

            Assert.IsTrue(repo.GetById(rec.Id).IsReturned);
            Assert.IsTrue(inv.GetAllBooks().First().IsAvailable);
        }

        [TestMethod]
        public void Vrati_ShouldThrow_WhenAlreadyReturned()
        {
            var svc = CreateService(out var users, out var inv, out var repo);

            svc.Posudi(1, 1);
            var rec = repo.GetHistory().First();

            svc.Vrati(rec.Id);

            Assert.ThrowsException<Exception>(() => svc.Vrati(rec.Id));
        }
    }
}
