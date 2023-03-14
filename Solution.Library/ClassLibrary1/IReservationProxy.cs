using BusinessLogic.Library.Entities;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public interface IReservationProxy
    {
        List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus);

        List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus);
    }
}
