using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Library;
using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library.VieModels;

namespace BusinessLogic.Library.Mappers
{
    public static class BookMapper
    {
        public static BookViewModel MapBookToViewModel(Book book)
        {
            BookViewModel bookViewModel = new BookViewModel()
            {
                ID = book.ID,
                Title = book.Title,
                AuthorName = book.AuthorName,
                AuthorSurname = book.AuthorSurname,
                PublishingHouse = book.PublishingHouse,
            };
            return bookViewModel;
        }

        public static Book MapViewModeltoBook(BookViewModel bookViewModel)
        {
            Book book = new Book()
            {
                ID = bookViewModel.ID,
                Title = bookViewModel.Title,
                AuthorName = bookViewModel.AuthorName,
                AuthorSurname = bookViewModel.AuthorSurname,
                PublishingHouse = bookViewModel.PublishingHouse,
                Quantity = bookViewModel.Quantity,

            };

            return book;
        }

        public static BookViewModel SearchBookVMToBookVM(SearchBookViewModel searchBookViewModel)
        {
            BookViewModel bookViewModel = new BookViewModel()
            {
                Title = searchBookViewModel.Title,
                AuthorName = searchBookViewModel.AuthorName,
                AuthorSurname = searchBookViewModel.AuthorSurname,
                PublishingHouse = searchBookViewModel.PublishingHouse,
            };
            return bookViewModel;
        }

        public static SearchBookViewModel BookVMToSearchBookVM(BookViewModel bookViewModel)
        {
            SearchBookViewModel searchBookViewModel = new SearchBookViewModel()
            {
                Title = bookViewModel.Title,
                AuthorName = bookViewModel.AuthorName,
                AuthorSurname = bookViewModel.AuthorSurname,
                PublishingHouse = bookViewModel.PublishingHouse,
            };
            return searchBookViewModel;
        }

        public static BookWithAvailabilityVM BookViewModelToAvailability(BookViewModel bookViewModel, List<Book> books/*, IEnumerable<Reservation> reservationList*/)
        {
            DateTime? firstAvailabilityDate = DateTime.Now;

            bool availability = true;
            if (books.Where(x => x.ID == bookViewModel.ID && x.Quantity == 0).Any())
            {
                availability = false;
            }
            
            BookWithAvailabilityVM vMbookWithAvailability = new BookWithAvailabilityVM()
            {
                ID = bookViewModel.ID,
                Title = bookViewModel.Title,
                AuthorName = bookViewModel.AuthorName,
                AuthorSurname = bookViewModel.AuthorSurname,
                PublishingHouse = bookViewModel.PublishingHouse,
                Quantity = bookViewModel.Quantity,
                IsDeleted = bookViewModel.IsDeleted,
                IsAvailable = availability, 
                FirstAvailabilityDate = firstAvailabilityDate,

            };
            return vMbookWithAvailability;
        }
        //reservationList.Where(r => r.Book.ID == bookViewModel.ID && r.EndDate < DateTime.Today).Count() < bookViewModel.Quantity
        //if IsAvailable == false, show EndDate > DateTime.Today()            
        //input of IsAvailable and FirstDate from Reservation
    }
}
