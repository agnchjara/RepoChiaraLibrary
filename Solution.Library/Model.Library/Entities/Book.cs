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
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public string AuthorSurname { get; set; }
        [DataMember]
        public string PublishingHouse { get; set; }

        /// <summary>
        /// Adesso rappresenta il numero di duplicati disponibili per la prenotazione
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
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
