using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library.InterfacesDAO
{
    public interface IUserDAO
    {
        User CreateUser(User user);
        User ReadUsernamePassword(string username, string password, List<User> users);
        List<User> ReadAllUsers();
        User UpdateUser();
        bool Delete(User user);
    }
}
