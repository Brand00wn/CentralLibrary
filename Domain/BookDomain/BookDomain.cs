using Domain.BookDomain.Model;
using Repository.Entities;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BookDomain
{
    public class BookDomain : IBookDomain
    {
        private readonly IUnitOfWork _uow;

        public BookDomain(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICollection<BookModel> GetBooks()
        {
            BookModel bookModel = new BookModel();
            return bookModel.ConvertToListModel(_uow.BookRepository.GetAll().ToList());
        }

        public ICollection<BookModel> GetLastAddedBooks(int take)
        {
            BookModel bookModel = new BookModel();
            return bookModel.ConvertToListModel(_uow.BookRepository.GetAll().OrderBy(o => o.CreationDate).Take(take).ToList());
        }
    }
}
