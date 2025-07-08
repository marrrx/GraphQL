using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Data.Exceptions;
using GraphQL.Data.Inputs;
using GraphQL.Data.Inputs.Create;
using GraphQL.Data.Inputs.Update;
using GraphQL.Model.Context;
using GraphQL.Model.Entities;
using Microsoft.EntityFrameworkCore;
using EntityLoan = GraphQL.Model.Entities.Loan;

namespace GraphQL.Data.Extensions.Loan
{
    [ExtendObjectType<Mutation>]
    public class LoanMutation
    {
        public async Task<EntityLoan> CreateLoan(
            ApplicationDbContext _context,
            CreateLoanInput loanInput
        )
        {
            var books = await _context
                .Books.Where(b => loanInput.bookIds.Contains(b.Id))
                .ToListAsync();

            if (books.Count != loanInput.bookIds.Count())
                throw new GraphQLException("Some books do not exist");

            var newLoan = new EntityLoan()
            {
                UserId = loanInput.userId,
                LoanDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMonths(3),
                LoanBooks = books.Select(b => new LoanBook { BookId = b.Id }).ToList(),
            };
            _context.Loans.Add(newLoan);
            await _context.SaveChangesAsync();
            return newLoan;
        }

        public async Task<EntityLoan> UpdateLoan(
            ApplicationDbContext _context,
            UpdateLoanInput loanInput,
            int id
        )
        {
            var loanToUpdate = await _context.Loans.FirstOrDefaultAsync(b => b.Id == id);

            loanToUpdate.ReturnDate = loanInput.returnDate;

            await _context.SaveChangesAsync();
            return loanToUpdate;
        }

        public async Task<DeleteResult> DeleteLoan(ApplicationDbContext _context, int id)
        {
            var loanToDelete = await _context.Loans.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (loanToDelete != null)
            {
                _context.Remove(loanToDelete);
            }
            await _context.SaveChangesAsync();

            var response = new DeleteResult { Success = true, DeleteBook = loanToDelete };
            return response;
        }
    }
}
