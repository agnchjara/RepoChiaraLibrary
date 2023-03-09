using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library
{
    /// <summary>
    /// Entità intermediaria tra chi vuole accedere ai dati e il DAL, che accede davvero ai dati
    /// </summary>
    public class Repository : IRepository
    {
        private static IBookDAO _IbookDAO;
        private static IUserDAO _IuserDAO;
        private static IReservationDAO _IreservationDAO;
        public Repository(IBookDAO ibookDAO, IUserDAO iuserDAO, IReservationDAO ireservationDAO)
        {
            _IbookDAO = ibookDAO;
            _IuserDAO = iuserDAO;
            _IreservationDAO = ireservationDAO;
        }

        public List<User> ReadAllUsers()
        {
            return _IuserDAO.ReadAllUsers();
        }

        //public User Login(string username, string password, List<User> users)
        //{
        //    User _user = _IuserDAO.ReadUsernamePassword(username, password, users);
        //    return _user;
        //}
        public List<Book> ReadAllBooks()
        {
            return _IbookDAO.ReadAllBooks();
        }

        public Book CreateBook(Book book)
        {
            _IbookDAO.CreateBook(book);
            return book;
        }

        public bool DeleteBook(Book book)
        {
            bool delete = false;
            _IbookDAO.DeleteBook(book);
            return delete = _IbookDAO.DeleteBook(book);
        }

        public Book UpdateBook(int bookId, Book bookWithNewValues)
        {
            _IbookDAO.UpdateBook(bookId, bookWithNewValues);
            return bookWithNewValues; //controlla se va bene questo come tipo di return
        }

        public List<Reservation> GetReservations()
        {
            return _IreservationDAO.ReadAllReservations();
        }

        public Reservation CreateReservation(Reservation reservation)
        {
            return _IreservationDAO.CreateReservation(reservation);
        }

        public bool DeleteReservation(Reservation reservation)
        {
            bool deleted = false;

            return  deleted = _IreservationDAO.DeleteReservation(reservation);
        }
    }
}
