using CentralLibrary.ViewModels.Book;
using CentralLibrary.ViewModels.User;
using Domain.BookDomain;
using Domain.BookDomain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using System.Data;
using System.Security.Claims;

namespace CentralLibrary.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Reader")]
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
            BorrowReturnModel returnModel = _bookDomain.Borrow(viewModel.Book.Id, userId);

            if (returnModel.Success)
            {
                TempData["SuccessMessage"] = returnModel.Message;
                return RedirectToAction("BookShelf");
            }
            else
            {
                TempData["ErrorBorrowMessage"] = returnModel.Message;
                return RedirectToAction("BorrowBook", new { bookId = viewModel.Book.Id });
            }
        }

        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> BorrowedBooks()
        {
            BorrowedBooksViewModel viewModel = new BorrowedBooksViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            viewModel.borrowedBooks.AddRange(_bookDomain.GetBorrowedBooks(userId));

            return View(viewModel);
        }
        
        public async Task<IActionResult> GiveBackBook(int bookId)
        {
            var book = _bookDomain.GetBook(bookId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _bookDomain.GiveBackBook(bookId, userId);

            TempData["SuccessMessage"] = $"The book {book.Title} returned to the shelf. Thank you and hope you had a fantastic reading!";

            return RedirectToAction("BorrowedBooks");
        }
    }
}
