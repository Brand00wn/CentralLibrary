using CentralLibrary.ViewModels;
using CentralLibrary.ViewModels.Book;
using Domain.BookDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System.Diagnostics;

namespace CentralLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookDomain _bookDomain;
        private readonly UserManager<User> _userManager;

        public HomeController(IBookDomain bookDomain, UserManager<User> userManager)
        {
            _bookDomain = bookDomain;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usuario = await _userManager.GetUserAsync(User);
                if (await _userManager.IsInRoleAsync(usuario, "Admin"))
                {
                    ManagementViewModel managementViewModel = new ManagementViewModel();
                    managementViewModel.ReturnUrl = "Book/BookRegistration";

                    return RedirectToAction("Management", "Book", managementViewModel);
                }
                else
                {
                    BookShelfViewModel bookShelfViewModel = new BookShelfViewModel();
                    bookShelfViewModel.books.AddRange(_bookDomain.GetBooks());

                    return RedirectToAction("BookShelf", "Book", bookShelfViewModel);
                }
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}