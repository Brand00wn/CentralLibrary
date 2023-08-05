using Domain.BookDomain.Model;

namespace CentralLibrary.ViewModels.Book
{
    public class BookShelfViewModel
    {
        public List<BookModel> books { get; set; } = new List<BookModel>();
    }
}
