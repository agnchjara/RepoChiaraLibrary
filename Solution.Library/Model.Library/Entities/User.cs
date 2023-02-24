using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    public class User
    {
        /// <summary>
        /// Numero identificativo univoco dell'utente
        /// </summary>
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }   
        public enumRole Role { get; set; }

        public User()
        { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }

    public enum enumRole
    {
        Admin,
        StandardUser
    }
}
