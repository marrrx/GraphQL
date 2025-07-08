using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Data.Exceptions;
using GraphQL.Data.Inputs;
using GraphQL.Model.Context;
using Microsoft.EntityFrameworkCore;
using EntityBook = GraphQL.Model.Entities.Book;
namespace GraphQL.Data.Extensions.Book
{
    [ExtendObjectType<Mutation>]
    public class BookMutations
    {
          public async Task<EntityBook> CreateBook(ApplicationDbContext _db, BookInput bookInput)
        {
            var newBook = new EntityBook()
            {
                Title = bookInput.Title,
                Year = bookInput.Year,
                AuthorId = bookInput.AuthorId
            };
            _db.Books.Add(newBook);
            await _db.SaveChangesAsync();
            return newBook;
        }

        public async Task<EntityBook> UpdateBook(ApplicationDbContext _db, BookInput bookInput, int id)
        {
            var bookToUpdate = await _db.Books
                                        .Where(b => b.Id == id)
                                        .FirstOrDefaultAsync();

            bookToUpdate.Title = bookInput.Title;
            bookToUpdate.Year = bookInput.Year;
            bookToUpdate.AuthorId = bookInput.AuthorId;

            await _db.SaveChangesAsync();
            return bookToUpdate;
        }

        public async Task<DeleteResult> DeleteBook(ApplicationDbContext _db, int id)
        {
            var bookToDelete = await _db.Books
                                        .Where(b => b.Id == id)
                                        .FirstOrDefaultAsync();

            if (bookToDelete != null)
            {
                _db.Remove(bookToDelete);
            }
            await _db.SaveChangesAsync();

            var response = new DeleteResult { Success = true, DeleteBook = bookToDelete };
            return response;
        }
    }
}