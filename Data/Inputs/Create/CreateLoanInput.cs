using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Data.Inputs.Create
{
    public record CreateLoanInput(int userId, IEnumerable<int>bookIds);
}