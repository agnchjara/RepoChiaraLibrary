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
        bool ReturnBook(int bookId, int userId);

        [OperationContract]
        List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus);

        [OperationContract]
        List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus);

        [OperationContract]
        BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel book);

       
  

        // TODO: Add your service operations here
    }


    //// Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
