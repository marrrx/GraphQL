using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace GraphQL.Model.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public ICollection<LoanBook> LoanBooks { get; set; }
    }
}
