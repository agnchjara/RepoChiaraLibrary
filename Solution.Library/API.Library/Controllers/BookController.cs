using Model.Library;
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
        // GET: api/Book?title=pippo&authorName=pluto& ecc                   
        public IEnumerable<string> Get(string title, string authorName, string authorSurname, string publishingHouse)
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Book/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Book
        public void Post([FromBody]string value)
        {
            //istanzio la bl e chiamo il metodo
        }

        [HttpPost]
        [Route("api/Book/Search")]
        public void Search([FromBody] Book book)
        {

        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
        }
    }
}
