using AutoMapper;
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
        private readonly IMapper _mapper;

        public BookDomain(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ICollection<BookModel> GetBooks()
        {
            ICollection<BookModel> bookModel;
            bookModel = _mapper.Map<ICollection<BookModel>>(_uow.BookRepository.GetAll().OrderByDescending(o => o.CreationDate).ToList());

            return bookModel;
        }

        public ICollection<BookModel> GetLastAddedBooks(int take)
        {
            ICollection<BookModel> bookModel;
            bookModel = _mapper.Map<ICollection<BookModel>>(_uow.BookRepository.GetAll().OrderBy(o => o.CreationDate).Take(take).ToList());

            return bookModel;
        }

        public BookModel GetBook(int bookId)
        {
            BookModel bookModel = new BookModel();
            bookModel = _mapper.Map<BookModel>(_uow.BookRepository.GetAll().First(f => f.Id == bookId));

            return bookModel;
        }

        public async Task Register(BookRegistrationModel model)
        {
            Book book = _mapper.Map<Book>(model);
            _uow.BookRepository.Create(book);
        }

        public async Task<DateTime> Borrow(int bookId, string userId)
        {
            return DateTime.Now;
        }
    }
}
