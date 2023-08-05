using Repository.EntityRepository.BookRepository;
using Repository.EntityRepository.BookRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, System.IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        private IBookRepository _bookRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBookRepository BookRepository { get => _bookRepository ?? (_bookRepository = new BookRepository(_context)); }

        protected virtual void Dispose(bool disposing)
        {
            if(!this._disposed && disposing) {
                _context.Dispose();
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
