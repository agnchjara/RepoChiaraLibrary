using BusinessLogic.Library;
using BusinessLogic.Library.Entities;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using DataAccessLayer.Library;
using DataAccessLayer.Library.EntitiesDB;
using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAPService.Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceLibrary" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceLibrary.svc or ServiceLibrary.svc.cs at the Solution Explorer and start debugging.
    public class ServiceLibrary : IServiceLibrary
    {
        public static IBookDAO BookDAO = new BookDAO_DB();
        public static IUserDAO UserDAO = new UserDAO_DB();
        public static IReservationDAO reservationDAO = new ReservationDAO_DB();
        public static IRepository repository = new Repository(BookDAO, UserDAO, reservationDAO);
        public static LibraryBusinessLogic lbl = new LibraryBusinessLogic(repository);

        #region
        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
        #endregion
        public Book AddBook(BookViewModel book)
        {
            return lbl.AddBook(book);
        }

        public bool DeleteBook(BookViewModel book)
        {
            return lbl.DeleteBook(book);
        }

        public List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus)
        {
            return lbl.GetReservationHistoryForAdmin(userViewModel, bookToReserve, reservationStatus);
        }

        public List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus)
        {
            return lbl.GetReservationsHistoryForStandardUser(bookToReserve, reservationStatus);
        }

        public UserViewModel Login(LoginViewModel loginVM)
        {
            return lbl.Login(loginVM);
        }

        public ReservationViewModel ReserveBook(BookWithAvailabilityVM book, UserViewModel user)
        {
            return lbl.ReserveBook(book, user);
        }

        public bool ReturnBook(int bookId, int userId)
        {
            return lbl.ReturnBook(bookId, userId);  
        }

        public List<BookViewModel> SearchBook(SearchBookViewModel book)
        {
            return lbl.SearchBook(book);
        }

        public BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel book)
        {
           return lbl.SearchBookWithAvailabilityInfos(book);
        }

        public BookViewModel UpdateBook(BookViewModel bookToSearch, BookViewModel bookWithNewValues)
        {
            return lbl.UpdateBook(bookToSearch, bookWithNewValues);
        }
    }
}
