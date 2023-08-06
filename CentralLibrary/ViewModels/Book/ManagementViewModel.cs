using Domain.BookDomain.Model;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CentralLibrary.ViewModels.Book
{
    public class ManagementViewModel
    {
        public string ReturnUrl { get; set; }
        public BookRegistrationModel BookRegistration { get; set; }
    }
}
