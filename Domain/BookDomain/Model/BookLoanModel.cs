using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BookDomain.Model
{
    public class BookLoanModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Returned { get; set; } = false;
    }
}
