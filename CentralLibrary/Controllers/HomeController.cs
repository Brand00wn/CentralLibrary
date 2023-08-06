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
                return RedirectToAction("BookShelf", "Book");
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