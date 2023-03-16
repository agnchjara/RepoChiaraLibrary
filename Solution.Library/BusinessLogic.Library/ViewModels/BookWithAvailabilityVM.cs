using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Library.Mappers
{
    [DataContract]
    public class BookWithAvailabilityVM
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
        /// rappresenta il numero di copie presenti in biblioteca
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public bool IsDeleted = false;
        [DataMember]
        public bool IsAvailable { get; set; }
        [DataMember]
        public DateTime? FirstAvailabilityDate { get; set; }

    }
}