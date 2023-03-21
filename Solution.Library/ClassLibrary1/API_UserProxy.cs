using Proxy.Library.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public class API_UserProxy : IUserProxy
    {
        public UserServiceModel Login(LoginServiceModel loginVM)
        {
            throw new NotImplementedException();
        }
    }
}
