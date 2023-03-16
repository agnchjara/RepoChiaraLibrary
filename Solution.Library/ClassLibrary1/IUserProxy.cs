using Proxy.Library.ServiceModels;
using Proxy.Library.SOAPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public interface IUserProxy
    {
        UserServiceModel Login(LoginServiceModel loginVM);
    }
}
