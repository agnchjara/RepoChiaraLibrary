using BusinessLogic.Library;
using BusinessLogic.Library.ViewModels;
using DataAccessLayer.Library.EntitiesDB;
using DataAccessLayer.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Library.Controllers
{
    public class UserController : ApiController
    {
        private static readonly IBookDAO BookDAO = new BookDAO_DB();
        private static readonly IUserDAO UserDAO = new UserDAO_DB();
        private static readonly IReservationDAO reservationDAO = new ReservationDAO_DB();
        private static readonly IRepository repository = new Repository(BookDAO, UserDAO, reservationDAO);
        LibraryBusinessLogic lbl = new LibraryBusinessLogic(repository);

        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/User/Login
        [HttpPost]
        [Route("api/User/Login")]
        public UserViewModel Login([FromBody] LoginViewModel loginVM)
        {
            return lbl.Login(loginVM);
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
