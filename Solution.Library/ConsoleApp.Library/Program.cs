using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BusinessLogic.Library;
using BusinessLogic.Library.ViewModels;
//using DataAccessLayer.Library;
//using Model.Library;
//using Model.Library.InterfacesDAO;

namespace ConsoleApp.Library
{
    public class Program
    {
        static void Main(string[] args)
        {
            //IBookDAO BookDAO = new BookDAO();
            //IUserDAO UserDAO = new UserDAO();
            //IReservationDAO reservationDAO = new ReservationDAO();
            //IRepository repository = new Repository(BookDAO, UserDAO,reservationDAO);
            LibraryBusinessLogic libraryBL = new LibraryBusinessLogic(); //qui arg "repository"

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
                LoginViewModel loginVM = new LoginViewModel()
                {
                    Username = userUsername,
                    Password = userPassword
                };
                
                UserViewModel loggedInUser = libraryBL.Login(loginVM);
                if (loggedInUser != null)
                {
                    LoginUI.SuccessfulLogin();
                    
                    if (loggedInUser.Role == Role.Admin)
                    {
                        Menu menuAdmin = new Menu();
                        menuAdmin.MenuAdmin(loggedInUser);
                    }
                    else
                    {
                        Menu menuStandardUser = new Menu();
                        menuStandardUser.MenuStandardUser(loggedInUser, loggedInUser.Username);
                    }
                }
                else
                {
                    LoginUI.LoginFailure();
                    Console.WriteLine("Do you want to try again? \nPress 'y' to enter your credentials again, press 'n' if you want to quit.");
                    string choice= Console.ReadLine();
                    if (choice == "n")
                        endApp = true;
                    if (choice == "y")
                        endApp = false;

                }
                
                
            } while (endApp == false);

            
           
            //LoginViewModel lvm = new LoginViewModel(username, password);
             
            //var userExist = libraryBusinessLogic.LoginUserCheck(lvm, listaUtentiXml);

            //userViewModel -> oggetto per fare il check su cosa può fare l'utente.
            //gli if non li facciamo in base allo user, ma allo user del view model.
        }
    }
}
