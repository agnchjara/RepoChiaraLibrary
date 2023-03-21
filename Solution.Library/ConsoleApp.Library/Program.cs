
using Proxy.Library;
using Proxy.Library.ServiceModels;
using System;
using System.Runtime.Remoting.Proxies;

namespace ConsoleApp.Library
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region DAOInterfaces
            //IBookDAO BookDAO = new BookDAO_DB();
            //IUserDAO UserDAO = new UserDAO_DB();
            //IReservationDAO reservationDAO = new ReservationDAO_DB();
            //IRepository repository = new Repository(BookDAO, UserDAO, reservationDAO);
            //LibraryBusinessLogic libraryBL = new LibraryBusinessLogic(repository);
            #endregion

            #region WCF SOAP
            //IBookProxy bookProxy = new WCF_BookProxy();
            //IUserProxy userProxy = new WCF_UserProxy();
            //IReservationProxy reservationProxy = new WCF_ReservationProxy();
            #endregion

            IBookProxy bookProxy = new API_BookProxy();
            IUserProxy userProxy = new API_UserProxy();
            IReservationProxy reservationProxy = new API_ReservationProxy();

            bool endApp = false;
            do
            {
                Console.WriteLine("*************************************************");
                Console.WriteLine("**                                             **");
                Console.WriteLine("**           INTERNATIONAL LIBRARY             **");
                Console.WriteLine("**                                             **");
                Console.WriteLine("**                                             **");
                Console.WriteLine("*************************************************");
                #region
                //List<User> users = repository.ReadAllUsers();
                //foreach (var user in users)
                //{
                //    Console.WriteLine(user.Username);
                //}
                #endregion
                Console.WriteLine("Please, enter your credentials below. \nUsername: ");
                string userUsername = Console.ReadLine();
                Console.WriteLine("Password: ");
                string userPassword = Console.ReadLine();
                LoginServiceModel loginSM = new LoginServiceModel()
                {
                    Username = userUsername,
                    Password = userPassword
                };

                
                UserServiceModel loggedInUser = userProxy.Login(loginSM);
                if (loggedInUser != null)
                {
                    LoginUI.SuccessfulLogin();
                    do
                    {
                        if (loggedInUser.Role == Role.Admin)
                        {
                            MenuAdmin menuAdmin = new MenuAdmin(/*repository*/);
                            menuAdmin.Menu(loggedInUser);

                        }
                        else
                        {
                            MenuStandardUser menuStandardUser = new MenuStandardUser(/*repository*/);
                            menuStandardUser.Menu(loggedInUser, loggedInUser.Username);
                        }

                        
                    } while (endApp == false);
                    
                }
                else
                {
                    LoginUI.LoginFailure();
                    Console.WriteLine("Do you want to try again? \nPress 'y' to enter your credentials again, press 'n' if you want to quit.");
                    string choice = Console.ReadLine();
                    if (choice == "n")
                        endApp = true;
                    if (choice == "y")
                        endApp = false;
                }

            } while (endApp == false);

        }
    }
}
