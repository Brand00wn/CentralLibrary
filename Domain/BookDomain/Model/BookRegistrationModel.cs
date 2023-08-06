using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.BookDomain.Model
{
    public class BookRegistrationModel
    {
        public int Id { get; set; }

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
        public string? ImageUrl { get; set; }
    }
}
