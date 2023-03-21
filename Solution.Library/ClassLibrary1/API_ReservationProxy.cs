using Proxy.Library.ServiceModels;
using Proxy.Library.SOAPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public class API_ReservationProxy : IReservationProxy
    {
        public List<ReservationServiceModel> GetReservationHistoryForAdmin(UserServiceModel userServiceModel, SearchBookServiceModel bookToReserve, ReservationStatus? reservationStatus)
        {
            throw new NotImplementedException();
        }

        public List<ReservationServiceModel> GetReservationsHistoryForStandardUser(SearchBookServiceModel bookToReserve, ReservationStatus reservationStatus)
        {
            throw new NotImplementedException();
        }

        public ReservationServiceModel ReserveBook(BookWithAvailabilityServiceModel book, UserServiceModel user)
        {
            throw new NotImplementedException();
        }

        public ReservationServiceModel ReturnBook(BookServiceModel book, UserServiceModel user)
        {
            throw new NotImplementedException();
        }

        public bool SearchActiveReservations_User(BookServiceModel bookViewModel, UserServiceModel userViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
