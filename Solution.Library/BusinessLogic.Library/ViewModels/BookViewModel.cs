using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library
{
    /// <summary>
    /// E' il Book come lo può vedere l'utente, non con tutte le informazioni
    /// </summary>
    [DataContract]
    public class BookViewModel
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
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }

        #region Costruttori
        public BookViewModel()
        {

        }

        public BookViewModel(string title, string authorName, string authorSurname, string publishingHouse)
        {
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;

        }
        public BookViewModel(string title, string authorName, string authorSurname, string publishingHouse, int quantity)
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
