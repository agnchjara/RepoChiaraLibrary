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
    public class WCF_UserProxy : IUserProxy
    {
        public UserServiceModel Login(LoginServiceModel loginSM)
        {
            ServiceLibraryClient slc = new ServiceLibraryClient();
            LoginViewModel loginVM = Mapper.MapLoginServiceModelToLoginViewModel(loginSM);
            UserViewModel userVM = slc.Login(loginVM);
            return Mapper.MapUserViewModelToUserServiceModel(userVM);
        }
    }
}
