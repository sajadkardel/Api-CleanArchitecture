using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Entities;

namespace GraphQL.Server.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBookAsync();
        Task<Book> GetByIdBookAsync(int bookId);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> RemoveBookAsync(Book book);
    }
}
