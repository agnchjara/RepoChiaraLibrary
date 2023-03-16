using Proxy.Library.Mappers;
using Proxy.Library.ServiceModels;
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
        ServiceLibraryClient slc = new ServiceLibraryClient();
        public List<ReservationServiceModel> GetReservationHistoryForAdmin(UserServiceModel userServiceModel, SearchBookServiceModel bookToReserve, ReservationStatus? reservationStatus)
        {
            UserViewModel userViewModel = Mapper.MapUserServiceModelToUserViewModel(userServiceModel);
            SearchBookViewModel searchBookViewModel = Mapper.MapSearchBookServiceModelToServiceBookViewModel(bookToReserve);
            List<ReservationViewModel> resVM = slc.GetReservationHistoryForAdmin(userViewModel, searchBookViewModel, reservationStatus);
            List<ReservationServiceModel> resSM = new List<ReservationServiceModel>();
            foreach(ReservationViewModel res in resVM)
            {
                var x = Mapper.MapReservationViewModelToReservationServiceModel(res);
                resSM.Add(x);
            }
                return resSM;
        }

        public List<ReservationServiceModel> GetReservationsHistoryForStandardUser(SearchBookServiceModel bookToReserve, ReservationStatus reservationStatus)
        {
            SearchBookViewModel searchBookViewModel = Mapper.MapSearchBookServiceModelToServiceBookViewModel(bookToReserve);
            List<ReservationViewModel> resVM = slc.GetReservationsHistoryForStandardUser(searchBookViewModel, reservationStatus);
            List<ReservationServiceModel> resSM = new List<ReservationServiceModel>();
            foreach (ReservationViewModel res in resVM)
            {
                var x = Mapper.MapReservationViewModelToReservationServiceModel(res);
                resSM.Add(x);
            }
            return resSM;
        }

        public ReservationServiceModel ReserveBook(BookWithAvailabilityServiceModel book, UserServiceModel user)
        {
            BookWithAvailabilityVM bookAv_VM = Mapper.MapBookWAvailabilityServiceModelToViewModel(book);
            UserViewModel user_VM = Mapper.MapUserServiceModelToUserViewModel(user);

            var x = slc.ReserveBook(bookAv_VM, user_VM);
            return Mapper.MapReservationViewModelToReservationServiceModel(x);
        }

        public ReservationServiceModel ReturnBook(BookServiceModel book, UserServiceModel user)
        {
            BookViewModel bookViewModel = Mapper.MapBookServiceModeltoViewModel(book);
            UserViewModel userViewModel = Mapper.MapUserServiceModelToUserViewModel(user);
            ReservationViewModel res_VM = slc.ReturnBook(bookViewModel, userViewModel);
            return Mapper.MapReservationViewModelToReservationServiceModel(res_VM);
        }

        public bool SearchActiveReservations_User(BookServiceModel bookViewModel, UserServiceModel userViewModel)
        {
            BookViewModel book_VM = Mapper.MapBookServiceModeltoViewModel(bookViewModel);   
            UserViewModel user_VM = Mapper.MapUserServiceModelToUserViewModel(userViewModel);
            bool x = slc.SearchActiveReservations_User(book_VM, user_VM);
            return x;
        }
    }
}
