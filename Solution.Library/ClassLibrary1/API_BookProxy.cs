using Model.Library;
using Newtonsoft.Json;
using Proxy.Library.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library
{
    public class API_BookProxy : IBookProxy
    {

        public Book AddBook(BookServiceModel book)
        {
            var serializedBook = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(serializedBook);
            Book _book = new Book();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:12345/API.Library/api/");
            //quando si chiama questo metodo parte la request
            var response = httpClient.PostAsync($"Book?title={book.Title}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;

                
            }
            else
            {
                //gestisco l'errore
            }


            return _book;
        }
       
        public bool DeleteBook(BookServiceModel book)
        {
            throw new NotImplementedException();
        }

        public ReservationServiceModel ReserveBook(BookWithAvailabilityServiceModel book, UserServiceModel user)
        {
            throw new NotImplementedException();
        }

        public List<BookServiceModel> SearchBook(SearchBookServiceModel book)
        {
            List<BookServiceModel> list = new List<BookServiceModel>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:12345/API.Library/api/");
            //quando si chiama questo metodo parte la request
            var response = httpClient.GetAsync($"Book?title={book.Title}").Result;
            if(response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;

                //deserializziamo
                list = JsonConvert.DeserializeObject < List <BookServiceModel>>(jsonContent);
            }
            else
            {
                //gestisco l'errore
            }
          

            return list;
        }

        public BookWithAvailabilityServiceModel SearchBookWithAvailabilityInfos(BookServiceModel book)
        {
            throw new NotImplementedException();
        }

        public BookServiceModel UpdateBook(BookServiceModel bookToSearch, BookServiceModel bookWithNewValues)
        {
            throw new NotImplementedException();
        }
    }
}
