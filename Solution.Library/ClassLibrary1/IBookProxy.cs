using Model.Library;
using Proxy.Library.ServiceModels;
using Proxy.Library.SOAPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public interface IBookProxy
    {
        Book AddBook(BookServiceModel book);

        BookServiceModel UpdateBook(BookServiceModel bookToSearch, BookServiceModel bookWithNewValues);

        bool DeleteBook(BookServiceModel book);

        ReservationServiceModel ReserveBook(BookWithAvailabilityServiceModel book, UserServiceModel user);

        List<BookServiceModel> SearchBook(SearchBookServiceModel book);   

        BookWithAvailabilityServiceModel SearchBookWithAvailabilityInfos(BookServiceModel book);
    }
}
