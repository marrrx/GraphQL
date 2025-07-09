using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Model.Context;
using Microsoft.EntityFrameworkCore;
using EntityLoan = GraphQL.Model.Entities.Loan;

namespace GraphQL.Data.Extensions.Loan
{
    [ExtendObjectType<Query>]
    public class LoanQueries
    {
        public async Task<List<EntityLoan>> GetLoans(ApplicationDbContext _context)
        {
            var loans = await _context
                .Loans.Include(x => x.User)
                .Include(l => l.LoanBooks)
                .ThenInclude(lb => lb.Book)
                .ThenInclude(b => b.Author)
                .ThenInclude(a => a.Nationality)
                .AsNoTracking()
                .OrderBy(b => b.Id)
                .ToListAsync();

            return loans;
        }

        public async Task<EntityLoan> GetLoanById(ApplicationDbContext _context, int id)
        {
            var loan = await _context
                .Loans.Include(x => x.User)
                .Include(l => l.LoanBooks)
                .ThenInclude(lb => lb.Book)
                .ThenInclude(b => b.Author)
                .ThenInclude(a => a.Nationality)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
            return loan;
        }
    }
}
