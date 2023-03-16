using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library
{
    [DataContract]
    public class ReservationViewModel
    {
        [DataMember]
        public User User { get; set; }
        [DataMember]
        public Book Book { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
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
