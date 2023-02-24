using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Library.InterfacesDAO;
using System.Xml.Linq;
using Model.Library;
using System.Xml;

namespace DataAccessLayer.Library
{
    public class UserDAO : IUserDAO
    {
       
        private string xmlFilePath = @"C:\Users\chiara.agnello\Desktop\materiale.studio\ProgettoBiblioteca\Solution.Library\DataAccessLayer.Library\DBXML.xml";
        public User CreateUser(User user)
        {
            try
            {
                XDocument doc = XDocument.Load(xmlFilePath);
                XElement library = doc.Element("Library");
                library.Add(new XElement("Users"),
                           new XElement("StandardUser", new XAttribute("UserID", user.ID), //.ToString() o no?
                           new XAttribute("Username", user.Username),
                           new XAttribute("Password", user.Password),
                           new XAttribute("Role", user.Role)));
                doc.Save(xmlFilePath);
                return user;
            }
            catch
            {
                throw new NotImplementedException();
            }
           
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Lettura di tutta la lista "users" di StandardUser
        /// </summary>
        /// <returns></returns>
        public List<User> ReadAllUsers()
        {
            List<User> result = new List<User>();
            
            XDocument doc = XDocument.Load(xmlFilePath);
            var userNodes = from userNode in doc.Root.Element("Users").Elements("User")
                            select userNode;
            foreach (XElement node in userNodes)
            {
                User user = new User();
                user.ID = int.Parse(node.Attribute("UserId").Value);
                user.Username = node.Attribute("Username").Value;
                user.Password = node.Attribute("Password").Value;
                user.Role = (enumRole)Enum.Parse(typeof(enumRole), node.Attribute("Role").Value);

                result.Add(user);
            }

            return result;
        }

        /// <summary>
        /// Restituisce il match di username&password passati, con l'utente della lista che ha quegli username&password corrispondenti
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public User ReadUsernamePassword(string username, string password, List<User> users)
        {
            var userForLogin = users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            return userForLogin;
           
        }

        public User UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
