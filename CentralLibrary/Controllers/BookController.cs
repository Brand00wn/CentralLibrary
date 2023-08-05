using CentralLibrary.ViewModels.Book;
using CentralLibrary.ViewModels.User;
using Domain.BookDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace CentralLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookDomain _bookDomain;

        public BookController(IBookDomain bookDomain)
        {
            _bookDomain = bookDomain;
        }

        [Authorize(Roles = "Reader, Admin")]
        public IActionResult BookShelf()
        {
            BookShelfViewModel viewModel = new BookShelfViewModel();
            viewModel.books.AddRange(_bookDomain.GetBooks());

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Management(ManagementViewModel viewModel)
        {
            viewModel.ReturnUrl = "Book/BookShelf";

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBook(ManagementViewModel viewModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                await _bookDomain.Register(viewModel.BookRegistration);

                return RedirectToAction("BookShelf");
            }

            return View();
        }

        public async Task<IActionResult> BorrowBook(int bookId, DateTime? dueDate = null)
        {
            BorrowBookViewModel viewModel = new BorrowBookViewModel();

            viewModel.Book = _bookDomain.GetBook(bookId);
            viewModel.ReturnUrl = "Book/BookShelf";

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBorrowBook(BorrowBookViewModel viewModel, string returnUrl = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dueDate = await _bookDomain.Borrow(viewModel.Book.Id, userId);

            TempData["SuccessMessage"] = $"Book borrowed successfully, you have until day {dueDate} to return it";

            return RedirectToAction("BorrowBook", viewModel.Book.Id);
        }
    }
}
