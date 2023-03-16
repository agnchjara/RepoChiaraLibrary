
using Model.Library;
using Proxy.Library.Mappers;
using Proxy.Library.ServiceModels;
using Proxy.Library.SOAPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public class WCF_BookProxy : IBookProxy
    {
        ServiceLibraryClient slc = new ServiceLibraryClient();

        public Book AddBook(BookServiceModel book)
        {
            return slc.AddBook(Mapper.MapBookServiceModeltoViewModel(book));
        }

        public bool DeleteBook(BookServiceModel book)
        {
            return slc.DeleteBook(Mapper.MapBookServiceModeltoViewModel(book));
        }

        public ReservationServiceModel ReserveBook(BookWithAvailabilityServiceModel book, UserServiceModel user)
        {
            BookWithAvailabilityVM b = Mapper.MapBookWAvailabilityServiceModelToViewModel(book);
            UserViewModel u  = Mapper.MapUserServiceModelToUserViewModel(user);
            var x = slc.ReserveBook(b, u);
            return Mapper.MapReservationViewModelToReservationServiceModel(x);
        }

        public List<BookServiceModel> SearchBook(SearchBookServiceModel book)
        {
            List<BookViewModel> books = slc.SearchBook(Mapper.MapSearchBookServiceModelToServiceBookViewModel(book));
            List<BookServiceModel> result = new List<BookServiceModel>();
            foreach(BookViewModel bookViewModel in books)
            {
                var x = Mapper.MapBookViewModeltoServiceModel(bookViewModel);
                result.Add(x);
            }
            return result;
        }

        public BookWithAvailabilityServiceModel SearchBookWithAvailabilityInfos(BookServiceModel book)
        {
            var b = slc.SearchBookWithAvailabilityInfos(Mapper.MapBookServiceModeltoViewModel(book));
            return Mapper.MapBookWAvailabilityToServiceModel(b);
        }

        public BookServiceModel UpdateBook(BookServiceModel bookToSearch, BookServiceModel bookWithNewValues)
        {
            BookViewModel book1 = Mapper.MapBookServiceModeltoViewModel(bookToSearch);
            BookViewModel newBook = Mapper.MapBookServiceModeltoViewModel(bookWithNewValues);
            slc.UpdateBook(book1, newBook);
            return bookWithNewValues;
        }
    }
}
