using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library.ServiceModels
{
    public class BookServiceModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        #region Costruttori
        public BookServiceModel()
        {

        }

        public BookServiceModel(string title, string authorName, string authorSurname, string publishingHouse)
        {
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;

        }
        public BookServiceModel(string title, string authorName, string authorSurname, string publishingHouse, int quantity)
        {
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;
            Quantity = quantity;

        }
        #endregion
        public override string ToString()
        {
            return $"Title: {this.Title} \nAuthor Name: {this.AuthorName} \nAuthor Surname: {this.AuthorSurname} \nPublisher: {this.PublishingHouse} \nQuantity: {this.Quantity} \nDeleted: {this.IsDeleted}.";
        }
    }
}
