using SmartLibrary.Data;
using SmartLibrary.Inventory; // ispravan namespace za Book
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartLibrary.Services
{
    public class RecommendationService
    {
        private BookRepository bookRepo;
        private BorrowHistoryRepository historyRepo;

        public RecommendationService(BookRepository bookRepo, BorrowHistoryRepository historyRepo)
        {
            this.bookRepo = bookRepo;
            this.historyRepo = historyRepo;
        }

        public List<Book> GetRecommendations(int userId)
        {
            var history = historyRepo.GetUserHistory(userId);
            if (!history.Any())
                return new List<Book>();

            // Najčitaniji žanr
            var favGenre = history
                .GroupBy(h => bookRepo.GetAllBooks().First(b => b.Id == h.BookId).Genre)
                .OrderByDescending(g => g.Count())
                .First().Key;

            // Sve knjige tog žanra
            var booksInGenre = bookRepo.GetBooksByGenre(favGenre);

            // Knjige koje korisnik već nije čitao
            var readBookIds = history.Select(h => h.BookId).ToHashSet();
            var recommendations = booksInGenre
                .Where(b => !readBookIds.Contains(b.Id))
                .Take(3)
                .ToList();

            return recommendations;
        }
    }
}
