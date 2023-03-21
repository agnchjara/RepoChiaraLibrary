using BusinessLogic.Library;
using BusinessLogic.Library.Mappers;
using BusinessLogic.Library.VieModels;
using DataAccessLayer.Library;
using DataAccessLayer.Library.EntitiesDB;
using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Library.Controllers
{
    public class BookController : ApiController
    {
        private static readonly IBookDAO BookDAO = new BookDAO_DB();
        private static readonly IUserDAO UserDAO = new UserDAO_DB();
        private static readonly IReservationDAO reservationDAO = new ReservationDAO_DB();
        private static readonly IRepository repository = new Repository(BookDAO, UserDAO, reservationDAO);
        LibraryBusinessLogic lbl = new LibraryBusinessLogic(repository);


        // GET: api/Book?title=pippo&authorName=pluto& ecc                   
        public IEnumerable<BookViewModel> Get(string title, string authorName, string authorSurname, string publishingHouse)
        {
            List<BookViewModel> fetchedbooks = lbl.SearchBook(new SearchBookViewModel()
            {
                Title = title,
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                PublishingHouse = publishingHouse

            });
            return fetchedbooks;
        }

        // POST: api/Book/SearchBookWithAvailabilityInfos
        [HttpPost]
        [Route("api/Book/SearchBookWithAvailabilityInfos")]
        public BookWithAvailabilityVM SearchBookWithAvailabilityInfos([FromBody] BookViewModel book)
        {
            return lbl.SearchBookWithAvailabilityInfos(book);
        }

        // POST: api/Book
        [HttpPost]
        [Route("api/Book/AddBook")]
        public Book AddBook([FromBody] BookViewModel book)
        {
             return lbl.AddBook(book);
        }

        [HttpPost]
        [Route("api/Book/SearchBook")]
        public List<BookViewModel> Search([FromBody] SearchBookViewModel book)
        {
            return lbl.SearchBook(book);
        }

        // POST: api/Book/
        [HttpPost]
        [Route("api/Book/UpdateBook")]
        public BookViewModel Update([FromBody] BookViewModel bookToSearch, BookViewModel bookWithNewValues)
        {
            return lbl.UpdateBook(bookToSearch, bookWithNewValues);
        }

        // DELETE: api/Book/Delete
        [HttpDelete]
        [Route("api/Book/DeleteBook")]
        public bool Delete([FromBody] BookViewModel book)
        {
            return lbl.DeleteBook(book);    
        }
        

       

       

       

   

        
    }
}
