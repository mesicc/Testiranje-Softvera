using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Data;
using SmartLibrary.Models;
using System;
using System.Linq;

namespace SmartLibrary.Tests
{
    [TestClass]
    public class BorrowRepositoryTests
    {
        [TestMethod]
        public void Add_ShouldAssignIncreasingIds()
        {
            var repo = new BorrowRepository();
            repo.Add(new BorrowRecord());
            repo.Add(new BorrowRecord());

            var list = repo.GetHistory().ToList();

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual(2, list[1].Id);
        }

        [TestMethod]
        public void GetActive_ShouldReturnOnlyNotReturnedItems()
        {
            var repo = new BorrowRepository();
            repo.Add(new BorrowRecord());
            repo.Add(new BorrowRecord { ReturnedDate = DateTime.Now });

            var active = repo.GetActive().ToList();

            Assert.AreEqual(1, active.Count);
            Assert.IsNull(active[0].ReturnedDate);
        }

        [TestMethod]
        public void GetByUser_ShouldReturnCorrectRecords()
        {
            var repo = new BorrowRepository();

            repo.Add(new BorrowRecord { UserId = 1 });
            repo.Add(new BorrowRecord { UserId = 2 });
            repo.Add(new BorrowRecord { UserId = 1 });

            var r = repo.GetByUser(1).ToList();

            Assert.AreEqual(2, r.Count);
            Assert.IsTrue(r.All(x => x.UserId == 1));
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectRecord()
        {
            var repo = new BorrowRepository();

            var r = new BorrowRecord { UserId = 10 };
            repo.Add(r);

            var found = repo.GetById(1);

            Assert.IsNotNull(found);
            Assert.AreEqual(10, found.UserId);
        }
    }
}
