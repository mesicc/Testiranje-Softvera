using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Inventory;
using System.Linq;

namespace SmartLibrary.Tests
{
    [TestClass]
    public class LibraryInventoryTests
    {
        [TestMethod]
        public void AddBook_ShouldAssignIdAndIncreaseCount()
        {
            var inv = new LibraryInventory();
            inv.AddBook("Test", "Autor", "Zanr");
            Assert.AreEqual(1, inv.GetAllBooks().Count);
            Assert.AreEqual(1, inv.GetAllBooks()[0].Id);
        }

        [TestMethod]
        public void UpdateBook_ShouldChangeFields_WhenBookExists()
        {
            var inv = new LibraryInventory();
            inv.AddBook("Old", "A", "X");
            inv.UpdateBook(1, "New", "B", "Y");

            var b = inv.GetAllBooks().First();

            Assert.AreEqual("New", b.Title);
            Assert.AreEqual("B", b.Author);
        }

        [TestMethod]
        public void DeleteBook_ShouldRemove_WhenExists()
        {
            var inv = new LibraryInventory();
            inv.AddBook("A", "B", "C");

            bool ok = inv.DeleteBook(1);

            Assert.IsTrue(ok);
            Assert.AreEqual(0, inv.GetAllBooks().Count);
        }

        [TestMethod]
        public void Search_ShouldReturnMatchingBooks()
        {
            var inv = new LibraryInventory();
            inv.AddBook("Alhemicar", "Coelho", "Roman");

            var result = inv.Search("alhem");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Alhemicar", result[0].Title);
        }
    }
}
