using CentralLibrary.ViewModels.Book;
using Domain.BookDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CentralLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookDomain _bookDomain;

        public BookController(IBookDomain bookDomain)
        {
            _bookDomain = bookDomain;
        }

        [Authorize(Roles = "Reader")]
        public IActionResult BookShelf()
        {
            BookShelfViewModel viewModel = new BookShelfViewModel();
            viewModel.books.AddRange(_bookDomain.GetBooks());

            return View(viewModel);
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Management(ManagementViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
