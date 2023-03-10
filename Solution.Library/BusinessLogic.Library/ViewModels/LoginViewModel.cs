using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.ViewModels
{
    [DataContract]
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
