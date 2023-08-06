using Domain.BookDomain.Model;

namespace CentralLibrary.ViewModels.Book
{
    public class BorrowedBooksViewModel
    {
        public List<BookModel> BorrowedBooks { get; set; } = new List<BookModel>();
    }
}
