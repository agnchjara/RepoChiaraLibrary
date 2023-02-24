using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; } 

        /// <summary>
        /// rappresenta il numero di copie acquistate dalla biblioteca
        /// </summary>
        public int Quantity { get; set; } 

        public bool IsDeleted = false;  
        public Book()
        {

        }
       
        public override string ToString()
        {
            return $"{this.Title}, {this.AuthorName} {this.AuthorSurname}, {this.PublishingHouse}.";
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
