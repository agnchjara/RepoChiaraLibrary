using BusinessLogic.Library;
using BusinessLogic.Library.Entities;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOAPService.Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IServiceLibrary
    {
        [OperationContract]
        UserViewModel Login(LoginViewModel loginVM);

        [OperationContract]
        Book AddBook(BookViewModel book);

        [OperationContract]
        List<BookViewModel> SearchBook(SearchBookViewModel book);

        [OperationContract]
        BookViewModel UpdateBook(BookViewModel bookToSearch, BookViewModel bookWithNewValues);

        [OperationContract]
        bool DeleteBook(BookViewModel book);

        [OperationContract]
        ReservationViewModel ReserveBook(BookWithAvailabilityVM book, UserViewModel user);

        [OperationContract]
        ReservationViewModel ReturnBook(BookViewModel bookVM, UserViewModel userVM);

        [OperationContract]
        List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus);

        [OperationContract]
        List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus);

        [OperationContract]
        BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel book);

        [OperationContract]
        bool SearchActiveReservations_User(BookViewModel bookViewModel, UserViewModel userViewModel);
    }

}
