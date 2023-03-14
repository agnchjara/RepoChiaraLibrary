using BusinessLogic.Library.Entities;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Library.EntitiesDB;
using DataAccessLayer.Library;
using Model.Library.InterfacesDAO;

namespace ConsoleApp.Library
{
    public class MenuAdmin
    {
        public static IBookDAO bookDAO = new BookDAO_DB();
        public static IUserDAO userDAO = new UserDAO_DB();
        public static IReservationDAO reservationDAO = new ReservationDAO_DB();
        public static IRepository repository = new Repository(bookDAO, userDAO, reservationDAO);
        public LibraryBusinessLogic lbl = new LibraryBusinessLogic(repository);
        public MenuAdmin(IRepository repository)
        {
            lbl = new LibraryBusinessLogic(repository);
        }

        public void Menu(UserViewModel user)
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
                        BookViewModel bookViewModel = new BookViewModel(title, authorName, authorSurname, publishingHouse, quantity);
                        lbl.AddBook(bookViewModel);

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
                        SearchBookViewModel bookVM = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        BookViewModel foundBook = lbl.SearchBook(bookVM).SingleOrDefault();
                        Console.WriteLine("  ");
                        Console.WriteLine("Now enter the new values. \nTitle: ");
                        string newTitle = Console.ReadLine();
                        Console.WriteLine("Author first name: ");
                        string newAuthorName = Console.ReadLine();
                        Console.WriteLine("Author last name: ");
                        string newAuthorSurname = Console.ReadLine();
                        Console.WriteLine("Publisher: ");
                        string newPublisher = Console.ReadLine();
                        BookViewModel newBookVM = new BookViewModel(newTitle, newAuthorName, newAuthorSurname, newPublisher);
                        BookViewModel updatedBook = lbl.UpdateBook(foundBook, newBookVM);

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
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        BookViewModel searchedBook = lbl.SearchBook(bookVMToSearch).FirstOrDefault();
                        lbl.DeleteBook(searchedBook);

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
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        List<BookViewModel> results = lbl.SearchBook(bookVMToSearch);

                        if (results == null)  //STA ROBA NON FUNZIONA
                        {
                            Console.WriteLine("This book doesn't exists.");
                        }
                        else
                        {
                            foreach (BookViewModel bookvm in results)
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
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        BookViewModel searchedBook = lbl.SearchBook(bookVMToSearch).FirstOrDefault();
                        if (searchedBook != null)
                        {
                            //Valuta la disponibilità del libro. Qui controlla se l'utente ha una res attiva per quel libro
                           
                            bool isThisBookReservedFromTheUser = lbl.SearchActiveReservations_User(searchedBook, user);
                            if (isThisBookReservedFromTheUser == false)
                            {
                                BookWithAvailabilityVM bookAvailable = lbl.SearchBookWithAvailabilityInfos(searchedBook);
                                if(bookAvailable != null)
                                {
                                    ReservationViewModel resCreated = lbl.ReserveBook(bookAvailable, user);
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
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        BookViewModel searchedBook = lbl.SearchBook(bookVMToSearch).FirstOrDefault();
                        if (searchedBook != null)
                        {
                            //Qui controlla se l'utente ha una res attiva per quel libro
                            bool isThisBookReservedFromTheUser = lbl.SearchActiveReservations_User(searchedBook, user);
                            if (isThisBookReservedFromTheUser == true)
                            {
                                ReservationViewModel returnBook = lbl.ReturnBook(searchedBook, user);
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
                        UserViewModel userViewModel = new UserViewModel()
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
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        Console.WriteLine("Reservation status (press the corresponding number): \n1. active \n2. not active. ");
                        string resStatus = Console.ReadLine();
                        ReservationStatus reservationStatus = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), resStatus);
                        List<BusinessLogic.Library.ReservationViewModel> result = lbl.GetReservationHistoryForAdmin(userViewModel, bookVMToSearch, reservationStatus);
                        foreach (BusinessLogic.Library.ReservationViewModel res in result)
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
