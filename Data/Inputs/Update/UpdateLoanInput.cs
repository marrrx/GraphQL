using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Data.Inputs.Update
{
    public record UpdateLoanInput([property: GraphQLType<DateType>] DateTime returnDate);
}
