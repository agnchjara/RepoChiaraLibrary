using Proxy.Library.ServiceModels;
using Proxy.Library.SOAPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public interface IReservationProxy
    {
        List<ReservationServiceModel> GetReservationHistoryForAdmin(UserServiceModel userServiceModel, SearchBookServiceModel bookToReserve, ReservationStatus? reservationStatus);

        List<ReservationServiceModel> GetReservationsHistoryForStandardUser(SearchBookServiceModel bookToReserve, ReservationStatus reservationStatus);
        ReservationServiceModel ReserveBook(BookWithAvailabilityServiceModel book, UserServiceModel user);

        ReservationServiceModel ReturnBook(BookServiceModel book, UserServiceModel user);

        bool SearchActiveReservations_User(BookServiceModel bookViewModel, UserServiceModel userViewModel);
    }
}
