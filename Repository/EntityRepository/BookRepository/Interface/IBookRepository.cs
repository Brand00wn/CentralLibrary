using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityRepository.BookRepository.Interface
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task DeleteBook(int bookId);
    }
}
