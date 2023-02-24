using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library
{
    public class ReservationViewModel
    {
        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ReservationViewModel(User user, Book book, DateTime startDate, DateTime endDate)
        {
            User = user;
            Book = book;
            StartDate = startDate;
            EndDate = endDate;
        }
        public ReservationViewModel()
        { }
        public override string ToString()
        {
            return $"{this.User}, {this.Book} {this.StartDate}, {this.EndDate}.";
        }
    }
}
