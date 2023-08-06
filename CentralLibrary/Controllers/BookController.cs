using AutoMapper;
using CentralLibrary.ViewModels.Book;
using CentralLibrary.ViewModels.User;
using Domain.BookDomain;
using Domain.BookDomain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using Repository.Entities;
using System.Data;
using System.Security.Claims;

namespace CentralLibrary.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookDomain _bookDomain;

        public BookController(IBookDomain bookDomain, IMapper mapper)
        {
            _bookDomain = bookDomain;
            _mapper = mapper;
        }

        [Authorize(Roles = "Reader, Admin")]
        public IActionResult BookShelf()
        {
            BookShelfViewModel viewModel = new BookShelfViewModel();

            viewModel.books.AddRange(_bookDomain.GetBooks());

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Management(ManagementViewModel viewModel, int? bookId)
        {
            viewModel.ReturnUrl = "Book/BookShelf";

            if (bookId.HasValue)
            {
                viewModel.BookRegistration = _mapper.Map<BookRegistrationModel>(_bookDomain.GetBook(bookId.Value));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBook(ManagementViewModel viewModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                if(viewModel.BookRegistration.Id > 0)
                {
                    await _bookDomain.Update(viewModel.BookRegistration);
                    TempData["SuccessMessage"] = $"Book {viewModel.BookRegistration.Title} updated with success!";
                }
                else
                {
                    await _bookDomain.Register(viewModel.BookRegistration);
                    TempData["SuccessMessage"] = $"Book {viewModel.BookRegistration.Title} registered with success!";
                }

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

        [Authorize(Roles = "Reader")]
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
        public async Task<IActionResult> MyBorrowedBooks()
        {
            BorrowedBooksViewModel viewModel = new BorrowedBooksViewModel();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            viewModel.BorrowedBooks.AddRange(_bookDomain.GetMyBorrowedBooks(userId));

            return View("BorrowedBooks", viewModel);
        }

        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GiveBackBook(int bookId)
        {
            var book = _bookDomain.GetBook(bookId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _bookDomain.GiveBackBook(bookId, userId);

            TempData["SuccessMessage"] = $"The book {book.Title} returned to the shelf. Thank you and hope you had a fantastic reading!";

            return RedirectToAction("MyBorrowedBooks");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveBook(int bookId)
        {
            var book = _bookDomain.GetBook(bookId);
            var bookLoan = _bookDomain.GetBookLoanByBookId(bookId).FirstOrDefault(w => !w.Returned);

            if(bookLoan != null)
            {
                TempData["ErrorMessage"] = $"The book {bookLoan.Book.Title} is borrowed, and you have to wait until {bookLoan.DueDate.ToShortDateString()} to delete it from the collection.";
                return RedirectToAction("BookShelf");
            }

            await _bookDomain.Remove(bookId);

            TempData["SuccessMessage"] = $"Book {book.Title} was deleted with success";

            return RedirectToAction("BookShelf");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptReceiving(int bookLoanId)
        {
            var bookLoan = _bookDomain.AcceptReceiving(bookLoanId);

            TempData["SuccessMessage"] = $"Book {bookLoan.Book.Title} received!";

            return RedirectToAction("BookShelf");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BorrowedBooks()
        {
            BorrowedBooksViewModel viewModel = new BorrowedBooksViewModel();

            viewModel.BorrowedBooks.AddRange(_bookDomain.GetBorrowedBooks());

            return View(viewModel);
        }
    }
}
