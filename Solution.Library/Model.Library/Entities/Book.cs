using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    [DataContract]
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; } 

        /// <summary>
        /// rappresenta il numero di duplicati disponibili per la prenotazione
        /// </summary>
        public int Quantity { get; set; } 

        public bool IsDeleted = false;  
        public Book()
        {

        }
       
        public override string ToString()
        {
            return $"Title: {this.Title}, \nAuthor Name: {this.AuthorName} \nAuthor Surname: {this.AuthorSurname}, \nPublisher: {this.PublishingHouse}, \nQuantity: {this.Quantity}, \nDeleted: {this.IsDeleted}.";
        }
        public override bool Equals(object obj)
        {
            if(obj != null)
            {
                Book that = obj as Book;
                return this.Title == that.Title && this.AuthorName == that.AuthorName && this.AuthorSurname == that.AuthorSurname && this.PublishingHouse == that.PublishingHouse;
            }
            return false;
        }
    }
}
