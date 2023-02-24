﻿using System;

namespace BusinessLogic.Library.Mappers
{
    public class BookWithAvailabilityVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; }

        /// <summary>
        /// rappresenta il numero di copie presenti in biblioteca
        /// </summary>
        public int Quantity { get; set; }  

        public bool IsDeleted = false;
        public bool IsAvailable { get; set; }
        public DateTime? FirstAvailabilityDate { get; set; }

    }
}