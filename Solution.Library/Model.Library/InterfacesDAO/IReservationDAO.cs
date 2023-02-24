
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library.InterfacesDAO
{ 
    public interface IReservationDAO
    {
        Reservation CreateReservation(Reservation reservation);
        List<Reservation> ReadAllReservations();
        Reservation UpdateReservation();
        bool DeleteReservation(Reservation reservation);
    }
}
