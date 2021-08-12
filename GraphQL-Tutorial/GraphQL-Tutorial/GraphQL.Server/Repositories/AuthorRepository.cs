using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Context;
using GraphQL.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Server.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthorAsync()
        {
            return await _context.Authors.AsNoTracking().ToListAsync();
        }

        public async Task<Author> GetByIdAuthorAsync(int authorId)
        {
            return await _context.Authors.SingleOrDefaultAsync(author => author.Id == authorId);
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            var addedAuthor = await _context.AddAsync(author);
            await _context.SaveChangesAsync();
            return addedAuthor.Entity;
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
            var updatedAuthor=_context.Update(author);
            await _context.SaveChangesAsync();
            return updatedAuthor.Entity;
        }

        public async Task<bool> RemoveAuthorAsync(Author author)
        {
            try
            {
                _context.Remove(author);
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
