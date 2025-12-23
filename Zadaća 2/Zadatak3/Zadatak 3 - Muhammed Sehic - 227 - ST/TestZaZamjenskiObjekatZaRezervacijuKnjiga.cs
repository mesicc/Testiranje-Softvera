using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Services;
using System.Linq;

[TestClass]
public class ReservationMockTests
{
    [TestMethod]
    public void TestMockReservationList()
    {
        var mock = new MockReservationService();
        var list = mock.GetUserReservations(10);

        Assert.IsNotNull(list);
        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(3, list.First());
    }

    [TestMethod]
    public void TestHasActiveReservation()
    {
        var mock = new MockReservationService();
        bool result = mock.HasActiveReservation(1, 2);

        Assert.IsTrue(result); // uvijek true će bude
    }
}
