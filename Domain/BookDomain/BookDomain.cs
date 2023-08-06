using AutoMapper;
using Domain.BookDomain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

        public List<BookModel> GetBooks()
        {
            List<BookModel> bookModel = new List<BookModel>();

            bookModel = _mapper.Map<List<BookModel>>(_uow.BookRepository.GetAll()
                .Include(i => i.BookLoans.Where(w => !w.Received))
                .ThenInclude(t => t.User)
                .OrderBy(o => o.BookLoans.Count(bl => !bl.Returned))
                .ThenBy(o => o.BookLoans.Count(bl => !bl.Received))
                .ThenByDescending(t => t.CreationDate).ToList());

            return bookModel;
        }

        public List<BookModel> GetLastAddedBooks(int take)
        {
            List<BookModel> bookModel = new List<BookModel>();

            bookModel = _mapper.Map<List<BookModel>>(_uow.BookRepository.GetAll()
                .OrderBy(o => o.CreationDate)
                .Take(take)
                .ToList());

            return bookModel;
        }

        public BookModel GetBook(int bookId)
        {
            BookModel bookModel = new BookModel();

            bookModel = _mapper.Map<BookModel>(_uow.BookRepository.GetAll()
                .First(f => f.Id == bookId));

            return bookModel;
        }

        public async Task Register(BookRegistrationModel model)
        {
            Book book = _mapper.Map<Book>(model);

            _uow.BookRepository.Create(book);
        }

        public async Task Update(BookRegistrationModel model)
        {
            Book book = _mapper.Map<Book>(model);

            _uow.BookRepository.Update(book);
        }

        public BorrowReturnModel Borrow(int bookId, string userId)
        {
            BorrowReturnModel returnModel = new BorrowReturnModel();

            var bookLoanByBookId = GetBookLoanByBookId(bookId);
            var book = GetBook(bookId);

            if (bookLoanByBookId == null || bookLoanByBookId.All(a => a.Returned))
            {
                var dueDate = DateTime.Now.AddMonths(2);

                BookLoanModel model = new BookLoanModel()
                {
                    BookId = bookId,
                    UserId = userId,
                    LoanDate = DateTime.Now,
                    DueDate = dueDate
                };

                BookLoan bookLoan = _mapper.Map<BookLoan>(model);

                _uow.BookLoanRepository.Create(bookLoan);

                returnModel.Success = true;
                returnModel.Message = $"You borrowed the book {book.Title} and have until {dueDate} to return it to us. Thank you, and have a good reading!";
            }
            else
            {
                returnModel.Success = false;
                returnModel.Message = $"This book is already borrowed, and will be available in {bookLoanByBookId?.FirstOrDefault(w => !w.Returned)?.DueDate}";
            }

            return returnModel;
        }

        public List<BookLoan> GetBookLoanByBookId(int bookId)
        {
            return _uow.BookLoanRepository.GetAll().Include(i => i.Book).Where(f => f.BookId == bookId).ToList();
        }

        public List<BookModel> GetMyBorrowedBooks(string userId)
        {
            List<BookModel> bookModel = new List<BookModel>();

            bookModel = _mapper.Map<List<BookModel>>(_uow.BookRepository.GetAll()
                .Include(i => i.BookLoans)
                .Where(w => w.BookLoans.Count(bl => !bl.Returned) > 0 && w.BookLoans.All(w => w.UserId == userId)).ToList());

            return bookModel;
        }

        public List<BookModel> GetBorrowedBooks()
        {
            List<BookModel> bookModel = new List<BookModel>();

            bookModel = _mapper.Map<List<BookModel>>(_uow.BookRepository.GetAll()
                .Include(i => i.BookLoans)
                .ThenInclude(i => i.User)
                .Where(w => w.BookLoans.Count(bl => !bl.Returned || !bl.Received) > 0).ToList());


            return bookModel;
        }

        public async Task GiveBackBook(int bookId, string userId)
        {
            var bookLoan = _uow.BookLoanRepository.GetAll()
                .First(w => w.BookId == bookId && !w.Returned && w.UserId == userId);

            bookLoan.Returned = true;

            _uow.BookLoanRepository.Update(bookLoan);
        }

        public async Task Remove(int bookId)
        {
            await _uow.BookRepository.DeleteBook(bookId);
        }

        public BookLoanModel AcceptReceiving(int bookLoanId)
        {
            BookLoanModel bookLoanModel = new BookLoanModel();

            var bookLoan = _uow.BookLoanRepository.GetAll().Include(i => i.Book).First(f => f.Id == bookLoanId);
            bookLoanModel = _mapper.Map<BookLoanModel>(bookLoan);

            bookLoan.Received = true;

            _uow.BookLoanRepository.Update(bookLoan);

            return bookLoanModel;
        }
    }
}
