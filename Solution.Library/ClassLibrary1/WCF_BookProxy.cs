
using BusinessLogic.Library;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
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
        public Book AddBook(BookViewModel book)
        {
            ServiceLibraryClient serviceLibraryClient = new ServiceLibraryClient();
            return serviceLibraryClient.AddBook(book);
        }

        public bool DeleteBook(BookViewModel book)
        {
            ServiceLibraryClient serviceLibraryClient= new ServiceLibraryClient();
            return serviceLibraryClient.DeleteBook(book);
        }

        public ReservationViewModel ReserveBook(BookWithAvailabilityVM book, UserViewModel user)
        {
            ServiceLibraryClient serviceLibraryClient = new ServiceLibraryClient();
            return serviceLibraryClient.ReserveBook(book, user);    
        }

        public List<BookViewModel> SearchBook(SearchBookViewModel book)
        {
            ServiceLibraryClient serviceLibraryClient = new ServiceLibraryClient(); 
            return serviceLibraryClient.SearchBook(book);
        }

        public BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel book)
        {
            ServiceLibraryClient serviceLibraryClient = new ServiceLibraryClient();
            return serviceLibraryClient.SearchBookWithAvailabilityInfos(book);
        }

        public BookViewModel UpdateBook(BookViewModel bookToSearch, BookViewModel bookWithNewValues)
        {
            ServiceLibraryClient serviceLibraryClient = new ServiceLibraryClient();
            return serviceLibraryClient.UpdateBook(bookToSearch, bookWithNewValues);
        }
    }
}
