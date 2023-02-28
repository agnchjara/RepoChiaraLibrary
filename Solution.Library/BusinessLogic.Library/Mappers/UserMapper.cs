using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Library.ViewModels;

namespace BusinessLogic.Library.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel MapUserToViewModel(User user)
        {
            UserViewModel userViewModel = new UserViewModel()
            {
                ID = user.ID,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role == Model.Library.Role.Admin ? ViewModels.Role.Admin : ViewModels.Role.StandardUser
            };
            return userViewModel;
        }
        public static User MapViewModelToUser (UserViewModel userViewModel)
        {
            User user = new User()
            {
                ID = userViewModel.ID,
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                Role = (Model.Library.Role)(userViewModel.Role == ViewModels.Role.Admin ? ViewModels.Role.Admin : ViewModels.Role.StandardUser)
            };
            return user;
        }

    }
}
