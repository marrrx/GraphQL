using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Model.Entities
{
    public class LoanBook
    {
        public int BookId { get; set; }
        public int LoanId { get; set; }
        public Book Book { get; set; }
        public Loan Loan { get; set; }

    }
}