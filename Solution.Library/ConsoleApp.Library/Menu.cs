using BusinessLogic.Library;
using BusinessLogic.Library.Entities;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Library
{
    public class Menu
    {
        public LibraryBusinessLogic lbl = new LibraryBusinessLogic();
        public Menu()
        {
            lbl = new LibraryBusinessLogic();
        }
    

        //public ILibraryBusinessLogic Lbl { get; set; }
        public void MenuAdmin(UserViewModel user)
        {
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
                        
                    } break;
                case "2":
                    {
                        Console.WriteLine("Enter the Title:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter the Author firs name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Enter the Author last name: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Enter the Publisher: ");
                        string publisher = Console.ReadLine();
                        SearchBookViewModel bookVM = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        var foundBook = lbl.SearchBook(bookVM);

                        Console.WriteLine("Now enter the new values. \nTitle: ");
                        string newTitle = Console.ReadLine();
                        Console.WriteLine("Author first name: ");
                        string newAuthorName = Console.ReadLine(); 
                        Console.WriteLine("Author last name: ");
                        string newAuthorSurname = Console.ReadLine();
                        Console.WriteLine("Publisher: ");
                        string newPublisher = Console.ReadLine();
                        BookViewModel newBookVM = new BookViewModel(newTitle, newAuthorName, newAuthorSurname, newPublisher);
                        BookViewModel updatedBook = lbl.UpdateBook(newBookVM);

                        if (updatedBook != null)
                        {
                            Console.WriteLine("Book successfully updated.");
                        }
                        else
                            Console.WriteLine("Something went wrong.");

                    } break;
                case "3":
                    {
                        //Delete di un libro
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

                    } break;
                case "4":
                    {
                        //Ricerca di un libro
                        Console.WriteLine("What book are you looking for? \nTitle:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher );
                        List<BookViewModel> results= lbl.SearchBook(bookVMToSearch);
                        foreach (BookViewModel bookvm in results)
                        {
                            Console.WriteLine(bookvm.ToString());
                        }
                        

                    }break;
                case "5":
                    {
                        //Prenotazione di un libro
                        Console.WriteLine("Enter the book's info you'd like to reserve. \n Title: ");
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
                            //Valuta la disponibilità del libro. 
                            BookWithAvailabilityVM isThisBookAvailable = lbl.SearchBookWithAvailabilityInfos(searchedBook);

                            if (isThisBookAvailable.IsAvailable == true)
                            {
                                ReservationViewModel resCreated = lbl.ReserveBook(isThisBookAvailable.ID, user.ID);
                                Console.WriteLine("You have reserved this book.");
                            }
                            else
                            {
                                //1. Il libro ha già una disponibilità attiva dall'utente => reservation is not possible

                                //2. Il libro non è disponibile.
                                Console.WriteLine("The book is not available. The first availability date is "+ isThisBookAvailable.FirstAvailabilityDate + ".");
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
                        //Restituzione di un libro

                    }break;
                case "7":
                    {
                        //Visualizza storico prenotazioni
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
                        foreach(BusinessLogic.Library.ReservationViewModel res in result)
                        {
                            res.ToString();  //così però non mostra se la res è active/notActive
                        }
                    }
                    break;
                case "8":
                    {
                        //Quit the program
                        Console.WriteLine("Goodbye, see you soon!");
                        Environment.Exit(0);

                    } break;
                default:
                    Console.WriteLine("Something went wrong.");
                    break;
            }    
        }

        public void MenuStandardUser(UserViewModel user, string username)
        {
            Console.WriteLine("Hello "+username+", what would you like to do?");
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
                        Console.WriteLine("What book are you looking for? \nTitle:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Author First Name: ");
                        string authorName = Console.ReadLine();
                        Console.WriteLine("Author Surname: ");
                        string authorSurname = Console.ReadLine();
                        Console.WriteLine("Publishing house: ");
                        string publisher = Console.ReadLine();
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        List<BookViewModel> results = lbl.SearchBook(bookVMToSearch);
                        foreach (BookViewModel bookvm in results)
                        {
                            Console.WriteLine(bookvm.ToString());
                        }
                    } break;
                case "2":
                    {
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
                        SearchBookViewModel bookVMToSearch = new SearchBookViewModel(title, authorName, authorSurname, publisher);
                        BookViewModel result = lbl.SearchBook(bookVMToSearch).SingleOrDefault();

                        if (result != null)
                        {
                            //Valuta la disponibilità del libro. 
                            BookWithAvailabilityVM isThisBookAvailable = lbl.SearchBookWithAvailabilityInfos(result);

                            if (isThisBookAvailable.IsAvailable == true)
                            {
                                ReservationViewModel resCreated  = lbl.ReserveBook(isThisBookAvailable.ID, user.ID);
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

                    } break;
                case "3":
                    {
                        //Return a book

                    }break;
                case "4":
                    {
                        //View Reservation History

                    }break;
                default:
                    Console.WriteLine("Something went wrong.");
                    break;
            }
        }

       public void SecondMenu()
        {
            Console.WriteLine("Press '1' if you wish to quit the application, press '2' if you want to logout, press '3' for further operations");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity))
            { Console.WriteLine("\nReenter the quantity, please: "); }
        }
        
    }
}
