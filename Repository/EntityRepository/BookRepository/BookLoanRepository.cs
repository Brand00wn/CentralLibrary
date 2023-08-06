using Repository.Entities;
using Repository.EntityRepository.BookRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityRepository.BookRepository
{
    public class BookLoanRepository : GenericRepository<BookLoan>, IBookLoanRepository
    {
        public BookLoanRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
