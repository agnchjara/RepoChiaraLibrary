using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Library;
using Model.Library.InterfacesDAO;

namespace DataAccessLayer.Library
{
    /// <summary>
    /// Interfaccia che definisce il comportamento dell'entità che si occuperà delle operazioni di lettura e scrittura
    /// </summary>
    public interface IRepository
    {
        List<User> ReadAllUsers();
        //User Login(string username, string password, List<User> users);

        List<Book> ReadAllBooks();
        Book CreateBook(Book book);
        bool DeleteBook(Book book);
        Book UpdateBook(int bookId, Book bookWithNewValues);
        Reservation CreateReservation(Reservation reservation);
        List<Reservation> GetReservations();
    }
}
