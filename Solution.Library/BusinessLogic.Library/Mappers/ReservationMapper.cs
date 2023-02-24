using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationViewModel MapReservationToViewModel(Reservation reservation)
        {
            ReservationViewModel rvm = new ReservationViewModel()
            {
                User = reservation.User,
                Book = reservation.Book,
                EndDate = reservation.EndDate, 
                StartDate = reservation.StartDate, 
            };
            return rvm;
        }
        public static Reservation MapViewModelToReservation(ReservationViewModel reservationViewModel)
        {
            Reservation reservation = new Reservation()
            {
                User = reservationViewModel.User,
                Book = reservationViewModel.Book,
                StartDate = reservationViewModel.StartDate,
                EndDate = reservationViewModel.EndDate,
            };
            return reservation;
        }
    }
}
