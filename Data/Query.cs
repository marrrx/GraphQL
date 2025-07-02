using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Model.Context;
using GraphQL.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<Book>> GetBook(ApplicationDbContext _context)
        {
            return await _context.Books
                                .Include(b => b.Author)
                                .ToListAsync();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Book> GetBookById(ApplicationDbContext _context, int id)
        {
            return await _context.Books
                                .Include(b => b.Author)
                                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}