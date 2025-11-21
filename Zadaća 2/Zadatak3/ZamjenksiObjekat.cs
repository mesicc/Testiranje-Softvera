using System.Collections.Generic;
using SmartLibrary.Models;

namespace SmartLibrary.Services
{
    // Originalni servis bi možda imao složenu logiku
    // Zamjenski objekt samo vraća fiksne rezultate
    public class MockRecommendationService
    {
        public List<Book> GetRecommendedBooksForUser(int userId)
        {
            // vraća fiksnu listu knjiga
            return new List<Book>
            {
                new Book { Title = "Kako postati GLUP", Author = "Kerim Gazić", Genre = "Fikcija" },
                new Book { Title = "Kako se obogatiti mlad", Author = "Muhammed Sehic", Genre = "Drama" }
            };
        }
    }
}
