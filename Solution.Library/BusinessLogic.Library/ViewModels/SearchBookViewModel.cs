using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.VieModels
{
    [DataContract]
    public class SearchBookViewModel
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public string AuthorSurname { get; set; }
        [DataMember]
        public string PublishingHouse { get; set; }

        public SearchBookViewModel(string title, string authorName, string authorSurname, string publishingHouse)
        {
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;
        }

        public SearchBookViewModel() { }
    }
}
