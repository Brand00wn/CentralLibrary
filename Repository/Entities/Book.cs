using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public bool Available { get; set; } = true;
        public string? Summary { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<BookLoan> BookLoans { get; set; }
    }
}
