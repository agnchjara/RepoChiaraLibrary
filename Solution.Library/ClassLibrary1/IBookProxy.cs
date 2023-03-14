using BusinessLogic.Library;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
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
        Book AddBook(BookViewModel book);

        BookViewModel UpdateBook(BookViewModel bookToSearch, BookViewModel bookWithNewValues);

        bool DeleteBook(BookViewModel book);

        ReservationViewModel ReserveBook(BookWithAvailabilityVM book, UserViewModel user);

        List<BookViewModel> SearchBook(SearchBookViewModel book);     //DONE

        BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel book);
    }
}
