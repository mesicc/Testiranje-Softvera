using System;
using System.Collections.Generic;

namespace SmartLibrary.Services
{
    // Zamjenski objekt koji kopira/mjenja/mockuje/zamjenjuje sistem rezervacija knjiga
    public class MockReservationService
    {
        public bool HasActiveReservation(int userId, int bookId)
        {
            // uvijek vraćamo fixni odgovor, pa tkd možemo slagat da ima knjiga rezervisanih kod našeg korisnika
            return true;
        }

        public List<int> GetUserReservations(int userId)
        {
            // vraćamo lažnu neku bezveze random listu rezervacija, made in BiH
            return new List<int> { 3, 8, 7 };
        }
    }
}
