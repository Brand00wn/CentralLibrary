using Domain.BookDomain.Model;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BookDomain
{
    public interface IBookDomain
    {
        ICollection<BookModel> GetBooks();
        ICollection<BookModel> GetLastAddedBooks(int take);
        Task Register(BookRegistrationModel model);
        BookModel GetBook(int bookId);
        Task<DateTime> Borrow(int bookId, string userId);
    }
}
