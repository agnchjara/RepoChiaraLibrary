using BusinessLogic.Library.Entities;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using BusinessLogic.Library.ViewModels;
using DataAccessLayer.Library;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Library
{
    public class LibraryBusinessLogic : ILibraryBusinessLogic
    {
        public IRepository Repository { get; set; }
       
        public LibraryBusinessLogic(IRepository repository)
        {
            Repository = repository;
        }

        public UserViewModel Login(LoginViewModel loginVM)
        {
            var fetchedUSers = Repository.ReadAllUsers();

            User utente = Login(loginVM.Username, loginVM.Password, fetchedUSers);

            if (utente != null)
            {
                return UserMapper.MapUserToViewModel(utente);
            }
            else
            {
                return null;
            }

        }
        public User Login(string username, string password, List<User> users)
        {
            User userForLogin = users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            return userForLogin;
        }
        public Book AddBook(BookViewModel bookViewModelToAdd)
        {
            SearchBookViewModel searchBookViewModel = BookMapper.BookVMToSearchBookVM(bookViewModelToAdd);
            List<BookViewModel> fetchedBooks = SearchBook(searchBookViewModel);
            //List<Book> fetchAllBooks = Repository.ReadAllBooks();
            //var fetchedBook = fetchAllBooks.FirstOrDefault(x => x.Title == book.Title &&
            //    x.AuthorName == book.AuthorName &&
            //    x.AuthorSurname == book.AuthorSurname
            //    && x.PublishingHouse == book.PublishingHouse);

            if (fetchedBooks.Count() == 0)
            {
                bookViewModelToAdd.ID = 1;
                Book book = BookMapper.MapViewModeltoBook(bookViewModelToAdd);
                Repository.CreateBook(book);
                return book;
            }
            else // (fetchAllBooks != null)
            {
                if (fetchedBooks.FirstOrDefault() == bookViewModelToAdd)
                {
                    bookViewModelToAdd.Quantity += bookViewModelToAdd.Quantity;
                    Book book = BookMapper.MapViewModeltoBook(UpdateBook(fetchedBooks.FirstOrDefault(), bookViewModelToAdd));
                    return book;
                }
                else // (fetchedBook == null)
                {
                    //Le 3 righe successive usate per l'XML
                    //int setId = fetchAllBooks.Select(b => b.ID).DefaultIfEmpty(0).Max() + 1;
                    //book.ID = setId;
                    Book book = BookMapper.MapViewModeltoBook(bookViewModelToAdd);
                    Repository.CreateBook(book);
                    return book;
                }
            }
            
        }
        public BookViewModel UpdateBook(BookViewModel foundBook, BookViewModel bookWithNewValues)
        {
            List<Book> bookResult = Repository.ReadAllBooks();

            Book toModify = bookResult.FirstOrDefault(x => x.ID == foundBook.ID);



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
        public ReservationViewModel ReserveBook(BookWithAvailabilityVM bookWavailability, UserViewModel userVM)
        {
            List<Book> books = Repository.ReadAllBooks();
            var fetchedBook = books.Where(x => x.ID == bookWavailability.ID).FirstOrDefault();

            List<User> users = Repository.ReadAllUsers();
            var fetchedUser = users.Where(x => x.ID == userVM.ID).FirstOrDefault();

            Reservation reservation = new Reservation(0, fetchedBook, fetchedUser, DateTime.Now);
            reservation.Book.ID = fetchedBook.ID;
            reservation.User.ID = fetchedUser.ID;
            //reservation.StartDate = DateTime.Now;

            #region Assegnazione ID Reservation + Diminuzione Book.Quantity -1 (PER XML)
            //List<Book> books = Repository.ReadAllBooks();
            //Reservation reservation = ReservationMapper.MapViewModelToReservation(reservation);
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
            ReservationViewModel resVM = ReservationMapper.MapReservationToViewModel(Repository.CreateReservation(reservation));

            return resVM;

        }
        public bool ReturnBook(int bookId, int userId)
        {
            bool returnedBook = false;
            List<Reservation> fetchedReservations = Repository.GetReservations();

            //questa non va bene perché ci possono essere più record di prenotazione dello stesso libro da parte dello stesso utente 
            Reservation match = fetchedReservations.Where(r => r.Book.ID == bookId && r.User.ID == userId).FirstOrDefault();
            match.EndDate = DateTime.Now;

            return returnedBook = Repository.DeleteReservation(match);
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

            //foreach (Book book in filteredBooks)
            //{
            //    filteredBooks.Where(b => b.IsDeleted != true).ToList();
            //}

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

            List<Book> books = SearchBooks(bookToSearch);
            List<BookViewModel> results = new List<BookViewModel>();
            foreach (Book book in books)
            {
                BookViewModel b = BookMapper.MapBookToViewModel(book);
                results.Add(b);
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
            //Questa lista viene passata nel mapper per controllare le prenotazioni attive su un libro
            //List<Reservation> fetchedReservations = Repository.GetReservations();

            List<Book> books = Repository.ReadAllBooks();
            BookWithAvailabilityVM bookWithAvailabilityInfos = BookMapper.BookViewModelToAvailability(bookToSearch, books/*, fetchedReservations*/);

            //if (fetchedReservations.Where(r => r.Book.ID == bookWithAvailabilityInfos.ID && r.StartDate > DateTime.Today)
            //if ((fetchedReservations.Where(r => r.Book.ID == bookWithAvailabilityInfos.ID && r.EndDate > DateTime.Today).Count() >= bookWithAvailabilityInfos.Quantity) && bookWithAvailabilityInfos.IsDeleted != true)
            //{
            //    bookWithAvailabilityInfos.IsAvailable = false;
            //    bookWithAvailabilityInfos.FirstAvailabilityDate = fetchedReservations.Where(r => r.Book.ID == bookWithAvailabilityInfos.ID && r.EndDate > DateTime.Today).OrderBy(r => r.EndDate).FirstOrDefault().EndDate;
            //}
            //else
            //{
            //    bookWithAvailabilityInfos.IsAvailable = true;
            //}

            #region
            //if (fetchedReservations.Count > 0)
            //foreach(Reservation item in fetchedReservations)
            //{
            //   GetReservationStatus(item); 
            //}
            //////Ritorna una lista di reservation di questo libro active alla data di oggi
            ////List<Reservation> reservedBooks = fetchedReservations.Where(r => r.ID == bookViewModelToAdd.ID && r.StartDate < DateTime.Today && r.EndDate > DateTime.Today).ToList();
            ////foreach (Reservation res in reservedBooks)
            ////{
            ////    if (res.EndDate == null)
            ////        res.EndDate = res.StartDate.AddDays(30);
            ////}

            //////Ritorna una lista con le reservation di questo libro che saranno active nel futuro
            ////List<Reservation> reservedBooksInTheFuture = fetchedReservations.Where(r => r.ID == bookViewModelToAdd.ID && r.StartDate > DateTime.Today).ToList();
            ////foreach (Reservation res in reservedBooksInTheFuture)
            ////{
            ////    if (res.StartDate > DateTime.Today)
            ////    {

            ////    }

            ////}
            #endregion

            return bookWithAvailabilityInfos;
        }



    }
}
