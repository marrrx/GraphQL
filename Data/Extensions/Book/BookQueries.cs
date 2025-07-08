using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Model.Context;
using Microsoft.EntityFrameworkCore;
using EntityBook = GraphQL.Model.Entities.Book;

namespace GraphQL.Data.Extensions.Book
{
    [ExtendObjectType<Query>]
    public class BookQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<EntityBook>> GetBooks(ApplicationDbContext _context)
        {
            return await _context
                .Books.Include(b => b.Author)
                .AsNoTracking()
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<EntityBook> GetBookById(ApplicationDbContext _context, int id)
        {
            return await _context
                .Books.Include(b => b.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
