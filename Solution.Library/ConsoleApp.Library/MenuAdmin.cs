using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Library.EntitiesDB;
using DataAccessLayer.Library;
using Model.Library.InterfacesDAO;
using Proxy.Library.ServiceModels;
using Proxy.Library.SOAPLibrary;
using Proxy.Library;

namespace ConsoleApp.Library
{
    public class MenuAdmin
    {
        //public static IBookDAO bookDAO = new BookDAO_DB();
        //public static IUserDAO userDAO = new UserDAO_DB();
        //public static IReservationDAO reservationDAO = new ReservationDAO_DB();
        //public static IRepository repository = new Repository(bookDAO, userDAO, reservationDAO);
        //public LibraryBusinessLogic lbl = new LibraryBusinessLogic(repository);

        //public MenuAdmin(IRepository repository)
        //{
        //    lbl = new LibraryBusinessLogic(repository);
        //}

        IBookProxy bookProxy = new WCF_BookProxy();
        IUserProxy userProxy = new WCF_UserProxy();
        IReservationProxy reservationProxy = new WCF_ReservationProxy();

        public void Menu(UserServiceModel user)
        {
            Console.WriteLine("  ");
            Console.WriteLine("Hello Admin, what do you want to do?");
            // Creazione di un libro
            Console.WriteLine("1. Add a new book.");
            // Modifica di un libro
            Console.WriteLine("2. Modify an existing book.");
            // Cancellazione di un libro
            Console.WriteLine("3. Delete a book.");
            // Ricerca di un libro
            Console.WriteLine("4. Search a book.");
            // Prenotazione di un libro
            Console.WriteLine("5. Reserve a book.");
            // Restituzione di un libro
            Console.WriteLine("6. Return a book.");
            // Visualizzazione Storico Prenotazioni
            Console.WriteLine("7. View reservation history.");
            // Uscita dal gestionale
            Console.WriteLine("8. Quit the program.");
            string choice = Console.ReadLine();
            #region ExceptionHandler
            //try
            //{
            //    int.Parse(choice);
            //}
            //catch (Exception ex)
            //{
            //    throw new InvalidCastException(ex.Message); 
            //}
            #endregion
            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("  ");
                        Console.WriteLine("Enter the title:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter the Author first name:");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Enter the Author last name:");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Enter the Publishing House:");
                        string publishingHouse = Console.ReadLine();
                        Console.WriteLine("How many copies? ");
                        int quantity;
                        while (!int.TryParse(Console.ReadLine(), out quantity))
                        { Console.WriteLine("\nReenter the quantity, please: "); }
                        BookServiceModel bookServiceModel = new BookServiceModel(title, authorName, authorSurname, publishingHouse, quantity);
                        bookProxy.AddBook(bookServiceModel);

                        Console.WriteLine("The book has been successfully added! \n ");

                    }
                    break;
                case "2":
                    {
                        Console.WriteLine("  ");
                        Console.WriteLine("Enter the Title:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter the Author firs name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Enter the Author last name: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Enter the Publisher: ");
                        string publisher = Console.ReadLine();
                        SearchBookServiceModel bookVM = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        BookServiceModel foundBook = bookProxy.SearchBook(bookVM).SingleOrDefault();
                        Console.WriteLine("  ");
                        Console.WriteLine("Now enter the new values. \nTitle: ");
                        string newTitle = Console.ReadLine();
                        Console.WriteLine("Author first name: ");
                        string newAuthorName = Console.ReadLine();
                        Console.WriteLine("Author last name: ");
                        string newAuthorSurname = Console.ReadLine();
                        Console.WriteLine("Publisher: ");
                        string newPublisher = Console.ReadLine();
                        BookServiceModel newBookVM = new BookServiceModel(newTitle, newAuthorName, newAuthorSurname, newPublisher);
                        BookServiceModel updatedBook = bookProxy.UpdateBook(foundBook, newBookVM);

                        if (updatedBook != null)
                        {
                            Console.WriteLine("Book successfully updated.");
                        }
                        else
                            Console.WriteLine("Something went wrong.");

                    }
                    break;
                case "3":
                    {
                        //Delete di un libro
                        Console.WriteLine("  ");
                        Console.WriteLine("What's the Book you'd like to delete? Enter the book information. \nTitle:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookServiceModel bookVMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        BookServiceModel searchedBook = bookProxy.SearchBook(bookVMToSearch).FirstOrDefault();
                        bookProxy.DeleteBook(searchedBook);

                    }
                    break;
                case "4":
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
                        Console.WriteLine("  ");
                        SearchBookServiceModel bookVMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        List<BookServiceModel> results = bookProxy.SearchBook(bookVMToSearch);

                        if (results == null)  //STA ROBA NON FUNZIONA
                        {
                            Console.WriteLine("This book doesn't exists.");
                        }
                        else
                        {
                            foreach (BookServiceModel bookvm in results)
                            {
                                Console.WriteLine(bookvm.ToString());
                                //Sarebbe carino qui mostrare i dati del BookWithAvailabilityVM con la firstAvailabilityDate4
                            }
                        }


                    }
                    break;
                case "5":
                    {
                        //Prenotazione di un libro
                        Console.WriteLine("  ");
                        Console.WriteLine("Enter the book's info you'd like to reserve. \nTitle: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookServiceModel bookVMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        BookServiceModel searchedBook = bookProxy.SearchBook(bookVMToSearch).FirstOrDefault();
                        if (searchedBook != null)
                        {
                            //Valuta la disponibilità del libro. Qui controlla se l'utente ha una res attiva per quel libro
                           
                            bool isThisBookReservedFromTheUser = reservationProxy.SearchActiveReservations_User(searchedBook, user);
                            if (isThisBookReservedFromTheUser == false)
                            {
                                BookWithAvailabilityServiceModel bookAvailable = bookProxy.SearchBookWithAvailabilityInfos(searchedBook);
                                if(bookAvailable != null)
                                {
                                    ReservationServiceModel resCreated = reservationProxy.ReserveBook(bookAvailable, user);
                                    Console.WriteLine("You have reserved this book. Please, remember to bring it back in 30 days.");
                                }
                                else
                                {
                                    Console.WriteLine("The book is not available. The first availability date is " + bookAvailable.FirstAvailabilityDate + ".");
                                }
                            }
                            else
                            {
                                // Il libro ha già una disponibilità attiva dall'utente => reservation is not possible
                                Console.WriteLine("You already have an active reservation for this book");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, there is no such book.");
                        }

                    }
                    break;
                case "6":
                    {
                        Console.WriteLine("  ");
                        Console.WriteLine("Enter the book's info you'd like to return. \nTitle: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookServiceModel bookVMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        BookServiceModel searchedBook = bookProxy.SearchBook(bookVMToSearch).FirstOrDefault();
                        if (searchedBook != null)
                        {
                            //Qui controlla se l'utente ha una res attiva per quel libro
                            bool isThisBookReservedFromTheUser = reservationProxy.SearchActiveReservations_User(searchedBook, user);
                            if (isThisBookReservedFromTheUser == true)
                            {
                                ReservationServiceModel returnBook = reservationProxy.ReturnBook(searchedBook, user);
                                Console.WriteLine("Thank you. You successfully returned this book.");
                            }
                            else
                            {
                                Console.WriteLine("You already have an active reservation for this book");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, there is no such book.");
                        }
                    }
                    break;
                case "7":
                    {
                        //Visualizza storico prenotazioni
                        Console.WriteLine("  ");
                        Console.WriteLine("Enter the reservation's information. \nUsername: ");
                        string username = Console.ReadLine();
                        UserServiceModel userServiceModel = new UserServiceModel()
                        {
                            Username = username,
                        };
                        //Ricerca di un libro
                        Console.WriteLine("Title:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookServiceModel bookVMToSearch = new SearchBookServiceModel(title, authorName, authorSurname, publisher);
                        Console.WriteLine("Reservation status (press the corresponding number): \n1. active \n2. not active. ");
                        string resStatus = Console.ReadLine();
                        ReservationStatus reservationStatus = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), resStatus);
                        List<ReservationServiceModel> result = reservationProxy.GetReservationHistoryForAdmin(userServiceModel, bookVMToSearch, reservationStatus);
                        foreach (ReservationServiceModel res in result)
                        {
                            res.ToString();  //così però non mostra se la res è active/notActive
                        }
                    }
                    break;
                case "8":
                    {
                        //Quit the program
                        Console.WriteLine("Goodbye, see you soon!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        //EndApp = true;
                    }
                    break;
                default:
                    Console.WriteLine("Something went wrong.");
                    break;
            }
        }
    }
}
