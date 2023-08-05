using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CentralLibrary.ViewModels.Book
{
    public class ManagementViewModel
    {
        public string ReturnUrl { get; set; }
        public BookRegistration BookRegistration { get; set; }
    }

    public class BookRegistration
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Display(Name = "ISBN")]
        public string? ISBN { get; set; }
        [Required]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }
        [Required]
        [Display(Name = "Pages")]
        public int Pages { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }
        [Display(Name = "Summary")]
        public string? Summary { get; set; }
    }
}
