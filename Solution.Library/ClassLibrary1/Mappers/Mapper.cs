
using Proxy.Library.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proxy.Library.SOAPLibrary;

namespace Proxy.Library.Mappers
{
    public static class Mapper
    {
        public static BookServiceModel MapBookViewModeltoServiceModel(BookViewModel bookViewModel)
        {
            BookServiceModel bookServiceModel = new BookServiceModel()
            {
                ID = bookViewModel.ID,
                Title = bookViewModel.Title,
                AuthorName = bookViewModel.AuthorName,
                AuthorSurname = bookViewModel.AuthorSurname,
                PublishingHouse = bookViewModel.PublishingHouse,
                Quantity = bookViewModel.Quantity,
                IsDeleted = bookViewModel.IsDeleted,
            };
            return bookServiceModel;
        }

        public static BookViewModel MapBookServiceModeltoViewModel(BookServiceModel bookServiceModel)
        {
            BookViewModel bookViewModel = new BookViewModel()
            {
                ID = bookServiceModel.ID,
                Title = bookServiceModel.Title,
                AuthorName = bookServiceModel.AuthorName,
                AuthorSurname = bookServiceModel.AuthorSurname,
                PublishingHouse = bookServiceModel.PublishingHouse,
                Quantity = bookServiceModel.Quantity,
                IsDeleted = bookServiceModel.IsDeleted,
            };
            return bookViewModel;
        }

        public static BookWithAvailabilityServiceModel MapBookWAvailabilityToServiceModel(BookWithAvailabilityVM bookWithAvailabilityVM)
        {
            BookWithAvailabilityServiceModel bookWithAvailabilitySM = new BookWithAvailabilityServiceModel()
            {
                ID = bookWithAvailabilityVM.ID,
                Title = bookWithAvailabilityVM.Title,
                AuthorName = bookWithAvailabilityVM.AuthorName,
                AuthorSurname = bookWithAvailabilityVM.AuthorSurname,
                PublishingHouse = bookWithAvailabilityVM.PublishingHouse,
                Quantity = bookWithAvailabilityVM.Quantity,
                IsDeleted = bookWithAvailabilityVM.IsDeleted,
                IsAvailable = bookWithAvailabilityVM.IsAvailable,
                FirstAvailabilityDate = bookWithAvailabilityVM.FirstAvailabilityDate,
            };
            return bookWithAvailabilitySM;
        }

        public static BookWithAvailabilityVM MapBookWAvailabilityServiceModelToViewModel(BookWithAvailabilityServiceModel bookWithAvailabilityServiceModel)
        {
            BookWithAvailabilityVM bookWithAvailabilitySM = new BookWithAvailabilityVM()
            {
                ID = bookWithAvailabilityServiceModel.ID,
                Title = bookWithAvailabilityServiceModel.Title,
                AuthorName = bookWithAvailabilityServiceModel.AuthorName,
                AuthorSurname = bookWithAvailabilityServiceModel.AuthorSurname,
                PublishingHouse = bookWithAvailabilityServiceModel.PublishingHouse,
                Quantity = bookWithAvailabilityServiceModel.Quantity,
                IsDeleted = bookWithAvailabilityServiceModel.IsDeleted,
                IsAvailable = bookWithAvailabilityServiceModel.IsAvailable,
                FirstAvailabilityDate = bookWithAvailabilityServiceModel.FirstAvailabilityDate,
            };
            return bookWithAvailabilitySM;
        }

        public static LoginViewModel MapLoginServiceModelToLoginViewModel(LoginServiceModel loginServiceModel)
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Username = loginServiceModel.Username,
                Password = loginServiceModel.Password,
            };
            return loginViewModel;
        }
        public static LoginServiceModel MapLoginViewModelToLoginServiceModel(LoginViewModel loginViewModel)
        {
            LoginServiceModel loginServiceModel = new LoginServiceModel()
            {
                Username = loginViewModel.Username,
                Password = loginViewModel.Password,
            };
            return loginServiceModel;
        }

        public static ReservationViewModel MapReservationServiceModelToReservationViewModel(ReservationServiceModel reservationServiceModel)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel()
            {
                User = reservationServiceModel.User,
                Book = reservationServiceModel.Book,
                StartDate = reservationServiceModel.StartDate,
                EndDate = reservationServiceModel.EndDate,
            };
            return reservationViewModel;
        }

        public static ReservationServiceModel MapReservationViewModelToReservationServiceModel(ReservationViewModel reservationViewModel)
        {
            ReservationServiceModel reservationServiceModel = new ReservationServiceModel()
            {
                User = reservationViewModel.User,
                Book = reservationViewModel.Book,
                StartDate = reservationViewModel.StartDate,
                EndDate = reservationViewModel.EndDate,
            };
            return reservationServiceModel;
        }

        public static SearchBookServiceModel MapSearchBookVieWModelToServiceBookServiceModel(SearchBookViewModel searchBookViewModel)
        {
            SearchBookServiceModel searchBookServiceModel = new SearchBookServiceModel()
            {
                Title = searchBookViewModel.Title,
                AuthorName = searchBookViewModel.AuthorName,
                AuthorSurname = searchBookViewModel.AuthorSurname,
                PublishingHouse = searchBookViewModel.PublishingHouse,
            };
            return searchBookServiceModel;
        }

        public static SearchBookViewModel MapSearchBookServiceModelToServiceBookViewModel(SearchBookServiceModel searchBookServiceModel)
        {
            SearchBookViewModel searchBookViewModel = new SearchBookViewModel()
            {
                Title = searchBookServiceModel.Title,
                AuthorName = searchBookServiceModel.AuthorName,
                AuthorSurname = searchBookServiceModel.AuthorSurname,
                PublishingHouse = searchBookServiceModel.PublishingHouse,
            };
            return searchBookViewModel;
        }

        public static UserServiceModel MapUserViewModelToUserServiceModel(UserViewModel userViewModel)
        {
            UserServiceModel userServiceModel = new UserServiceModel()
            {
                ID = userViewModel.ID,
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                Role = (ServiceModels.Role)userViewModel.Role
            };
            return userServiceModel;
        }
        public static UserViewModel MapUserServiceModelToUserViewModel(UserServiceModel userServiceModel)
        {
            UserViewModel userViewModel = new UserViewModel()
            {
                ID = userServiceModel.ID,
                Username = userServiceModel.Username,
                Password = userServiceModel.Password,
                Role = (SOAPLibrary.Role)userServiceModel.Role
            };
            return userViewModel;
        }
    }
}
