using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Entities;

namespace GraphQL.Server.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthorAsync();
        Task<Author> GetByIdAuthorAsync(int authorId);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Author author);
        Task<bool> RemoveAuthorAsync(Author author);
    }
}
