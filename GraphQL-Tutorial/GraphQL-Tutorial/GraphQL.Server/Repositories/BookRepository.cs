using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Context;
using GraphQL.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Server.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBookAsync()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetByIdBookAsync(int bookId)
        {
            return await _context.Books.SingleOrDefaultAsync(book => book.Id == bookId);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            var addedBook = await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return addedBook.Entity;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            var updatedBook = _context.Update(book);
            await _context.SaveChangesAsync();
            return updatedBook.Entity;
        }

        public async Task<bool> RemoveBookAsync(Book book)
        {
            try
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
