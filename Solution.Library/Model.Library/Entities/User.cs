using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    [DataContract]
    public class User
    {
        /// <summary>
        /// Numero identificativo univoco dell'utente
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]   
        public Role Role { get; set; }

        public User()
        { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
    
    public enum Role
    {
        Admin,
        StandardUser
    }
}
