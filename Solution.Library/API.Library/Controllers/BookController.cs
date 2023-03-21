using BusinessLogic.Library;
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

        // GET: api/Book/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Book
        public void Post([FromBody] string value)
        {

            //istanzio la bl e chiamo il metodo
        }

        [HttpPost]
        [Route("api/Book/Search")]
        public void Search([FromBody] Book book)
        {
            //var lbl = new LibraryBusinessLogic()
        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
        }
    }
}
