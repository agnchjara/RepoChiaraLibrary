using DataAccessLayer.Library.EntitiesDB;
using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library
{
    public class UserDAO_DB : IUserDAO
    {
        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Users";

                    //to receive the data from the previous command
                    SqlDataReader reader = cmd.ExecuteReader();

                    //reader legge i dati in sequenza 
                    while (reader.Read())
                    {
                        int id = Int32.Parse(reader["UserId"].ToString());
                        enumRole Role = (enumRole)Enum.Parse(typeof(enumRole), reader["Role"].ToString());
                        string username = reader["Username"].ToString();
                        string password = reader["Password"].ToString();


                        User user = new User()
                        {
                            ID = id,
                            Role = Role,
                            Username = username,
                            Password = password,
                        };
                        users.Add(user);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return users;
        }

        public User ReadUsernamePassword(string username, string password, List<User> users)
        {
            User userForLogin = users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            return userForLogin;
        }

        public User UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
