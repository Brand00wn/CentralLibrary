using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BookDomain.Model
{
    public class BookModel : Book
    {
        public List<BookModel> ConvertToListModel(List<Book> book) {
            
            List<BookModel> bookModels = new List<BookModel>();
            book.ForEach(f => bookModels.Add(ConvertToModel(f)));
            return bookModels;
        }

        public BookModel ConvertToModel(Book book)
        {
            return new BookModel()
            {
                Author = book.Author,
                Available = book.Available,
                BookLoans = book.BookLoans,
                Genre = book.Genre,
                Id = book.Id,
                ISBN = book.ISBN,
                Pages = book.Pages,
                PublicationDate = book.PublicationDate,
                Summary = book.Summary,
                Title = book.Title
            };
        }
    }
}
