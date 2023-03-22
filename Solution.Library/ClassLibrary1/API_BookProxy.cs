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
            //book parametrizzato da serializzare in stringa json
            string serializedBook = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(serializedBook);
            Book _book = new Book();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost/API.Library/api/");
            //quando si chiama questo metodo parte la request
            HttpResponseMessage response = httpClient.PostAsync($"Book?title={book.Title}&authorName={book.AuthorName}&" +
                $"authorSurname={book.AuthorSurname}&publishingHouse={book.PublishingHouse}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;
                //deserializziamo
                _book = JsonConvert.DeserializeObject<Book>(jsonContent);
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
            httpClient.BaseAddress = new Uri("http://localhost/API.Library/api/");
            //quando si chiama questo metodo parte la request
            var response = httpClient.GetAsync($"Book?title={book.Title}&authorName={book.AuthorName}&authorSurname={book.AuthorSurname}&publishingHouse={book.PublishingHouse}").Result;
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

            //book parametrizzato da serializzare in stringa json
            string jsonBookWithNewValues = JsonConvert.SerializeObject(bookWithNewValues);
            StringContent content1 = new StringContent(jsonBookWithNewValues);
            string jsonBookToSearch = JsonConvert.SerializeObject(bookWithNewValues);
            StringContent content2 = new StringContent(jsonBookToSearch);

            BookServiceModel _book = new BookServiceModel();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost/API.Library/api/");
            //quando si chiama questo metodo parte la request
            HttpResponseMessage response = httpClient.PostAsync($"Book?title={bookToSearch.Title}&authorName={bookToSearch.AuthorName}&" +
                $"authorSurname={bookToSearch.AuthorSurname}&publishingHouse={bookToSearch.PublishingHouse}&quantity={bookToSearch.Quantity}&isDeleted={bookToSearch.IsDeleted}", content2).Result;
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
    }
}
