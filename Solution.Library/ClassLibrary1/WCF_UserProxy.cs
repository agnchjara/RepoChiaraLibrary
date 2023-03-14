using BusinessLogic.Library.ViewModels;
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
        public UserViewModel Login(LoginViewModel loginVM)
        {
            ServiceLibraryClient serviceLibraryClient = new ServiceLibraryClient();
            return serviceLibraryClient.Login(loginVM);
        }
    }
}
