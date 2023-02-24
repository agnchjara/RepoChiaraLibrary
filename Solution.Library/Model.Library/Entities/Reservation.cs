using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    public class Reservation
    {
        public int ID { get; set; }
        public Book Book { get; set; }

        public User User { get; set; }
        public DateTime StartDate { get; set; }
        /// <summary>
        /// L’EndDate deve essere valorizzata nel momento in cui viene effettuata la prenotazione come StartDate + 30 GG e aggiornata all’atto della restituzione del libro.
        /// </summary>
        public DateTime? EndDate { get; set; }

        public Reservation(int iD, Book book, User user, DateTime startDate, DateTime? endDate = null)
        {
            ID = iD;
            Book = book;
            User = user;
            StartDate = startDate;
            EndDate = endDate ?? startDate.AddDays(30);
        }

        public Reservation()
        {

        }
    }
   
}
