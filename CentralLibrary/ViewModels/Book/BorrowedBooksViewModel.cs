using Domain.BookDomain.Model;

namespace CentralLibrary.ViewModels.Book
{
    public class BorrowedBooksViewModel
    {
        public List<BookModel> borrowedBooks { get; set; } = new List<BookModel>();
    }
}
