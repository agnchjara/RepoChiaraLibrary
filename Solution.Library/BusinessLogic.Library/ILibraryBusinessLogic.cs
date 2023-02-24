using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using Model.Library;
using BusinessLogic.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Library.Mappers;

namespace BusinessLogic.Library
{
    public interface ILibraryBusinessLogic
    {
        UserViewModel Login(LoginViewModel loginVM);
        Book AddBook(BookViewModel book); 
        List<BookViewModel> SearchBook(SearchBookViewModel book);
        BookViewModel UpdateBook(BookViewModel bookWithNewValues); 
        bool DeleteBook(BookViewModel book);
        //StandardUser GetUserByUserName(string userName); questo forse è quello che ho chiamato "CheckIfAdmin"
        Reservation CreateReservation(ReservationViewModel reservation);
        ReservationResult ReserveBook(int bookId, int userId);

        //ReservationResult ReturnBook(int bookId, int userId);
        List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus);
        List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus);
        BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel book);
    }
}
