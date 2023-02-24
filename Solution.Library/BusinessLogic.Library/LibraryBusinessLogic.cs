using Model.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Library;
using Model.Library.InterfacesDAO;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.Mappers;
using System.ComponentModel;
using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library.Entities;
using DataAccessLayer.Library.EntitiesDB;

namespace BusinessLogic.Library
{
    public class LibraryBusinessLogic : ILibraryBusinessLogic
    {
        public Repository Repository { get; set; }       //era IRepository prima

        public LibraryBusinessLogic(Repository repository)
        {
            Repository = repository;
        }
        public LibraryBusinessLogic()
        {
            IBookDAO bookDAO = new BookDAO_DB();
            IUserDAO userDAO = new UserDAO();
            IReservationDAO reservationDAO = new ReservationDAO(bookDAO, userDAO);
            Repository = new Repository(bookDAO, userDAO, reservationDAO);

        }
        public UserViewModel Login(LoginViewModel loginVM)
        {
            var fetchedUSers = Repository.ReadAllUsers();

            User utente = Repository.Login(loginVM.Username, loginVM.Password, fetchedUSers);

            if (utente != null)
            {
                return UserMapper.MapUserToViewModel(utente);
            }
            else
            {
                return null;
            }

        }
        public Book AddBook(BookViewModel bookViewModel)
        {
            Book book = BookMapper.MapViewModeltoBook(bookViewModel);
            List<Book> fetchAllBooks = Repository.ReadAllBooks();
            var fetchedBook = fetchAllBooks.FirstOrDefault(x => x.Title == book.Title &&
                x.AuthorName == book.AuthorName &&
                x.AuthorSurname == book.AuthorSurname
                && x.PublishingHouse == book.PublishingHouse);

            if (fetchAllBooks.Count() == 0)
            {
                book.ID = 1;
                Repository.CreateBook(book);

            }
            else // (fetchAllBooks != null)
            {
                if (fetchedBook == book)
                {
                    book.Quantity += fetchedBook.Quantity;
                    BookViewModel fetchedBookToVM = BookMapper.MapBookToViewModel(fetchedBook);
                    BookViewModel bookToVM = BookMapper.MapBookToViewModel(book);
                    UpdateBook(bookToVM);
                }
                else // (fetchedBook == null)
                {
                    //Le 3 righe successive usate per l'XML
                    //int setId = fetchAllBooks.Select(b => b.ID).DefaultIfEmpty(0).Max() + 1;
                    //book.ID = setId;
                    Repository.CreateBook(book);
                }
            }
            return book;

        }
        public BookViewModel UpdateBook(BookViewModel bookWithNewValues)
        {
            List<Book> bookResult = Repository.ReadAllBooks();

            Book toModify = bookResult.FirstOrDefault(x => x.ID == bookWithNewValues.ID);



            Book bookModified = BookMapper.MapViewModeltoBook(bookWithNewValues);
            Repository.UpdateBook(bookWithNewValues.ID, bookModified);

            return bookWithNewValues;
        }
        public bool DeleteBook(BookViewModel bookVM)
        {
            bool delete = false;
            if (bookVM != null)
            {
                Book book = BookMapper.MapViewModeltoBook(bookVM);
                delete = Repository.DeleteBook(book);
            }

            return delete;
        }
        public Reservation CreateReservation(ReservationViewModel reservationViewModel)
        {
            //List<Book> books = Repository.ReadAllBooks();
            Reservation reservation = ReservationMapper.MapViewModelToReservation(reservationViewModel);
            #region Assegnazione ID Reservation + Diminuzione Book.Quantity -1 (PER XML)
            //Reservation reservation = ReservationMapper.MapViewModelToReservation(reservationViewModel);
            //List<Reservation> fetchedReservations = Repository.GetReservations();
            //int querySetId = fetchedReservations.Select(r => r.ID).DefaultIfEmpty(0).Max() + 1;
            //reservation.ID = querySetId;

            //var updateQuantityOfReservedBook = (from r in fetchedReservations
            //                                    from b in books
            //                                    where r.Book.ID == b.ID
            //                                    select b).FirstOrDefault();

            //updateQuantityOfReservedBook.Quantity--;
            //Repository.UpdateBook(updateQuantityOfReservedBook.ID, updateQuantityOfReservedBook);
            #endregion

            return Repository.CreateReservation(reservation);
            
        }
        private List<Book> SearchBooks(SearchBookViewModel bookToSearch)
        {
            List<Book> books = Repository.ReadAllBooks();
            List<Book> filteredBooks = books;

            if (!string.IsNullOrEmpty(bookToSearch.Title))
            {
                filteredBooks = filteredBooks.Where(x => x.Title == bookToSearch.Title).ToList();
            }
            if (!string.IsNullOrEmpty(bookToSearch.AuthorName))
            {
                filteredBooks = filteredBooks.Where(x => x.AuthorName == bookToSearch.AuthorName).ToList();
            }
            if (!string.IsNullOrEmpty(bookToSearch.AuthorSurname))
            {
                filteredBooks = filteredBooks.Where(x => x.AuthorSurname == bookToSearch.AuthorSurname).ToList();
            }
            if (!string.IsNullOrEmpty(bookToSearch.PublishingHouse))
            {
                filteredBooks = filteredBooks.Where(x => x.PublishingHouse == bookToSearch.PublishingHouse).ToList();
            }

            return filteredBooks;

        }

        /// <summary>
        /// Metodo per l'implementazione della Ricerca di un libro tramite parametri opzionali
        /// </summary>
        /// <param name="book"></param>
        /// <returns>Lista di Book</returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<BookViewModel> SearchBook(SearchBookViewModel bookToSearch)
        {
            List<BookViewModel> results = new List<BookViewModel>();

            List<Book> books = SearchBooks(bookToSearch);

            foreach (Book book in books)
            {
                var bvm = BookMapper.MapBookToViewModel(book);
                results.Add(bvm);
            }


            return results;
        }

        public List<ReservationViewModel> GetReservationHistoryForAdmin(UserViewModel userViewModel, SearchBookViewModel bookToReserve, ReservationStatus? reservationStatus)
        {
            List<ReservationViewModel> result = new List<ReservationViewModel>();
            List<Reservation> reservations = GetReservationsHistory(userViewModel, bookToReserve, reservationStatus);

            foreach (Reservation item in reservations)
            {
                var rvm = ReservationMapper.MapReservationToViewModel(item);
                result.Add(rvm);
            }

            return result; //da passare anche questa al Program per farla stampare
        }

        private ReservationStatus GetReservationStatus(Reservation reservation)
        {
            ReservationStatus reservationStatus = ReservationStatus.notActive;
            if (reservation == null)
            {
                throw new InvalidOperationException();
            }
            if (reservation.EndDate?.Date == null)
            {
                reservation.EndDate?.AddDays(30);
            }
            if (DateTime.Now.Date > reservation.StartDate.Date && DateTime.Now.Date < reservation.EndDate?.Date)
            {
                reservationStatus = ReservationStatus.active;
            }
            if (reservation.StartDate > DateTime.Today)
            {
                reservationStatus = ReservationStatus.active;
                Console.WriteLine("The book is available, but has a reservation starting from the day ", reservation.StartDate.Date);
            }

            return reservationStatus;
        }
        private List<Reservation> GetReservationsHistory(UserViewModel userViewModel = null, SearchBookViewModel bookToReserve = null, ReservationStatus? reservationStatus = null) //con questo =null accetta anche che questi parametri siano null
        {
            User user = UserMapper.MapViewModelToUser(userViewModel);
            Book book = SearchBooks(bookToReserve).FirstOrDefault();
            List<Reservation> reservations = Repository.GetReservations();
            List<Reservation> filteredReservations = reservations;
            List<User> utenti = Repository.ReadAllUsers();

            if (!string.IsNullOrEmpty(user.Username))
            {
                filteredReservations = filteredReservations.Where(x => x.User.Username == user.Username).ToList();
            }
            if (book != null)
            {
                filteredReservations = filteredReservations.Where(x => x.Book.Equals(book)).ToList();
            }
            if (reservationStatus != null)
            {
                filteredReservations = filteredReservations.Where(x => reservationStatus == GetReservationStatus(x)).ToList();
            }
            
            return filteredReservations;

        }
        public List<ReservationViewModel> GetReservationsHistoryForStandardUser(SearchBookViewModel bookToReserve, ReservationStatus reservationStatus)
        {
            List<ReservationViewModel> result = new List<ReservationViewModel>();
            List<Reservation> reservations = GetReservationsHistory(null, bookToReserve, reservationStatus);

            foreach (Reservation item in reservations)
            {
                var rvm = ReservationMapper.MapReservationToViewModel(item);
                result.Add(rvm);
            }

            return result; //da passare anche questa al Program per farla stampare
        }

        public BookWithAvailabilityVM SearchBookWithAvailabilityInfos(BookViewModel bookToSearch)
        {
            //Questa lista viene passata nel mapper per calcolare la FirstAvailabilityDate
            List<Reservation> fetchedReservations = Repository.GetReservations();

            BookWithAvailabilityVM bookWithAvailabilityInfos = BookMapper.BookViewModelToAvailability(bookToSearch, fetchedReservations);

            #region
            //if (fetchedReservations.Count > 0)
            //foreach(Reservation item in fetchedReservations)
            //{
            //   GetReservationStatus(item); 
            //}
            //////Ritorna una lista di reservation di questo libro active alla data di oggi
            ////List<Reservation> reservedBooks = fetchedReservations.Where(r => r.ID == bookViewModel.ID && r.StartDate < DateTime.Today && r.EndDate > DateTime.Today).ToList();
            ////foreach (Reservation res in reservedBooks)
            ////{
            ////    if (res.EndDate == null)
            ////        res.EndDate = res.StartDate.AddDays(30);
            ////}

            //////Ritorna una lista con le reservation di questo libro che saranno active nel futuro
            ////List<Reservation> reservedBooksInTheFuture = fetchedReservations.Where(r => r.ID == bookViewModel.ID && r.StartDate > DateTime.Today).ToList();
            ////foreach (Reservation res in reservedBooksInTheFuture)
            ////{
            ////    if (res.StartDate > DateTime.Today)
            ////    {

            ////    }

            ////}
            #endregion
     
            return bookWithAvailabilityInfos;
        }

        public ReservationResult ReserveBook(int bookId, int userId)
        {
            
        }

    }
}
