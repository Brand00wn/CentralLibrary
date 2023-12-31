﻿using Domain.BookDomain.Model;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BookDomain
{
    public interface IBookDomain
    {
        List<BookModel> GetBooks();
        List<BookModel> GetLastAddedBooks(int take);
        Task Register(BookRegistrationModel model);
        BookModel GetBook(int bookId);
        BorrowReturnModel Borrow(int bookId, string userId);
        List<BookModel> GetMyBorrowedBooks(string userId);
        Task GiveBackBook(int bookId, string userId);
        List<BookLoan> GetBookLoanByBookId(int bookId);
        Task Remove(int bookId);
        Task Update(BookRegistrationModel model);
        BookLoanModel AcceptReceiving(int bookLoanId);
        List<BookModel> GetBorrowedBooks();
    }
}
