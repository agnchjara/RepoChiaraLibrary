using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library.InterfacesDAO
{
    public interface IBookDAO
    {
        Book CreateBook(Book book);
        List<Book> ReadAllBooks();
        Book UpdateBook(int bookId, Book bookWithNewValues);
        bool DeleteBook(Book book);

    }
}
