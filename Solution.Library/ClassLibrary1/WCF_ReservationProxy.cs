using BusinessLogic.Library;
using BusinessLogic.Library.Entities;
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
    public class WCF_ReservationProxy : IReservationProxy
    {
        public List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus)
        {
            ServiceLibraryClient slc = new ServiceLibraryClient();
            return slc.GetReservationHistoryForAdmin(userViewModel, bookToReserve, reservationStatus);
        }

        public List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus)
        {
            ServiceLibraryClient slc = new ServiceLibraryClient();
            return slc.GetReservationsHistoryForStandardUser(bookToReserve, reservationStatus);
        }
    }
}
