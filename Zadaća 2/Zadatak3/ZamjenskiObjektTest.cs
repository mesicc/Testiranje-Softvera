using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartLibrary.Services;
using System.Linq;

[TestClass]
public class RecommendationTests
{
    [TestMethod]
    public void TestMockRecommendations()
    {
        var mockService = new MockRecommendationService();
        var recommendations = mockService.GetRecommendedBooksForUser(1);

        Assert.IsNotNull(recommendations);
        Assert.AreEqual(2, recommendations.Count);
        Assert.AreEqual("Misli i obogati se", recommendations.First().Title);
    }
}
