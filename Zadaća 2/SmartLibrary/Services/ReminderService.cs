using System;
using SmartLibrary.Data;
using SmartLibrary.Inventory;
using SmartLibrary.Models;

namespace SmartLibrary.Services
{
    public class ReminderService
    {
        private readonly BorrowRepository _borrowRepo;
        private readonly UserRepository _userRepo;
        private readonly LibraryInventory _inventory;

        public ReminderService(BorrowRepository borrowRepo, UserRepository userRepo, LibraryInventory inventory)
        {
            _borrowRepo = borrowRepo;
            _userRepo = userRepo;
            _inventory = inventory;
        }

        public void PosaljiPodsjetnike()
        {
            Console.WriteLine("\n=== Podsjetnici za rokove ===");

            var today = DateTime.Now;
            var active = _borrowRepo.GetActive();

            bool ima = false;

            foreach (var r in active)
            {
                var user = _userRepo.GetById(r.UserId);
                var book = _inventory.GetAllBooks().Find(b => b.Id == r.BookId);

                int daysLeft = (r.DueDate - today).Days;

                if (daysLeft == 1)
                {
                    Console.WriteLine($"[EMAIL] -> {user.Email} | Rok za knjigu '{book.Title}' ističe sutra ({r.DueDate.ToShortDateString()})");
                    ima = true;
                }
                else if (daysLeft == 0)
                {
                    Console.WriteLine($"[EMAIL] -> {user.Email} | Rok za '{book.Title}' je danas! ({r.DueDate.ToShortDateString()})");
                    ima = true;
                }
                else if (daysLeft < 0)
                {
                    Console.WriteLine($"[EMAIL] -> {user.Email} | Knjiga '{book.Title}' kasni {Math.Abs(daysLeft)} dana! Rok je bio {r.DueDate.ToShortDateString()}");
                    ima = true;
                }
            }

            if (!ima)
                Console.WriteLine("Nema podsjetnika za slanje.");
        }
    }
}
