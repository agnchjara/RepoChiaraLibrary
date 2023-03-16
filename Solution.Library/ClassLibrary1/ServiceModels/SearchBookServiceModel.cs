using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Proxy.Library.ServiceModels
{
    public class SearchBookServiceModel
    {
        public string Title { get; set; }
        
        public string AuthorName { get; set; }
       
        public string AuthorSurname { get; set; }
       
        public string PublishingHouse { get; set; }

        public SearchBookServiceModel(string title, string authorName, string authorSurname, string publishingHouse)
        {
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;
        }

        public SearchBookServiceModel() { }
    }
}
