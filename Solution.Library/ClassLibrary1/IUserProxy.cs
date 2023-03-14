using BusinessLogic.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public interface IUserProxy
    {
        UserViewModel Login(LoginViewModel loginVM);
    }
}
