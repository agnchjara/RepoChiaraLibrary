

using Proxy.Library;
using Proxy.Library.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

namespace ConsoleApp.Library
{
    public class MenuStandardUser
    {

        //public static IBookDAO bookDAO = new BookDAO_DB();
        //public static IUserDAO userDAO = new UserDAO_DB();
        //public static IReservationDAO reservationDAO = new ReservationDAO_DB();
        //public static IRepository repository = new Repository(bookDAO, userDAO, reservationDAO);
        //public LibraryBusinessLogic lbl = new LibraryBusinessLogic(repository);
        //public MenuStandardUser(IRepository repository)
        //{
        //    lbl = new LibraryBusinessLogic(repository);
        //}

        IBookProxy bookProxy = new WCF_BookProxy();
        IUserProxy userProxy = new WCF_UserProxy();
        IReservationProxy reservationProxy = new WCF_ReservationProxy();

        public void Menu(UserServiceModel user, string username)
        {
            Console.WriteLine("  ");
            Console.WriteLine("Hello " + username + ", what would you like to do?");
            // Ricerca 
            Console.WriteLine("1. Search a book.");
            // Prenotazione di un libro
            Console.WriteLine("2. Reserve a book.");
            // Restituzione di un libro
            Console.WriteLine("3. Return a book.");
            // Reservation History
            Console.WriteLine("4. View you reservations' history.");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        //Ricerca di un libro
                        Console.WriteLine("  ");
                        Console.WriteLine("What book are you looking for? \nTitle:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookServiceModel bookSMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        List<BookServiceModel> results = bookProxy.SearchBook(bookSMToSearch);
                        if (results == null)  //STA ROBA NON FUNZIONA
                        {
                            Console.WriteLine("This book doesn't exists.");
                        }
                        else
                        {
                            foreach (BookServiceModel booksm in results)
                            {
                                Console.WriteLine(booksm.ToString());
                                //Sarebbe carino qui mostrare i dati del BookWithAvailabilityServiceModel con la firstAvailabilityDate4
                            }
                        }
                    }
                    break;
                case "2":
                    {
                        Console.WriteLine("  ");
                        Console.WriteLine("Enter the book's info you'd like to reserve");
                        Console.WriteLine("Title: ");
                        string title = Console.ReadLine();
                        while (string.IsNullOrEmpty(title))
                        { Console.WriteLine("\nTitle cannot be empty. Reenter the title, please: "); }
                        Console.WriteLine("Author Name: ");
                        string authorName = Console.ReadLine();
                        while (string.IsNullOrEmpty(authorName))
                        { Console.WriteLine("\nAuthor Name cannot be empty. Reenter the name, please: "); }
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        while (string.IsNullOrEmpty(authorSurname))
                        { Console.WriteLine("\nAuthor Surname cannot be empty. Reenter the surname, please: "); }
                        Console.WriteLine("Publisher: ");
                        string publisher = Console.ReadLine();
                        while (string.IsNullOrEmpty(publisher))
                        { Console.WriteLine("\nPublisher cannot be empty. Reenter the publisher, please: "); }
                        SearchBookServiceModel bookSMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        BookServiceModel result = bookProxy.SearchBook(bookSMToSearch).SingleOrDefault();

                        if (result != null)
                        {
                            //Valuta la disponibilità del libro. 
                            BookWithAvailabilityServiceModel isThisBookAvailable = bookProxy.SearchBookWithAvailabilityInfos(result);

                            if (isThisBookAvailable.IsAvailable == true)
                            {
                                ReservationServiceModel resCreated = reservationProxy.ReserveBook(isThisBookAvailable, user);
                                Console.WriteLine("You have reserved this book.");
                            }
                            else
                            {
                                //1. Il libro ha già una disponibilità attiva dall'utente => reservation is not possible

                                //2. Il libro non è disponibile. 
                                Console.WriteLine("The book is not available. The first availability date is " + isThisBookAvailable.FirstAvailabilityDate + ".");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The book you requested doesn't exist. Sorry!");
                        }

                    }
                    break;
                case "3":
                    {
                        Console.WriteLine("Wh");

                    }
                    break;
                case "4":
                    {
                        //View Reservation History

                    }
                    break;
                default:
                    Console.WriteLine("Something went wrong.");
                    break;
            }
        }

        public void SecondMenu()
        {
            Console.WriteLine("  ");
            Console.WriteLine("Press '1' if you wish to quit the application, press '2' if you want to logout, press '3' for further operations");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity))
            { Console.WriteLine("\nReenter the quantity, please: "); }
        }

    }
}
