using Domain.BookDomain.Model;

namespace CentralLibrary.ViewModels.Book
{
    public class BorrowBookViewModel
    {
        public BookModel Book { get; set; }
        public string ReturnUrl { get; set; }
        public DateTime DueDate { get; set; }
    }
}
