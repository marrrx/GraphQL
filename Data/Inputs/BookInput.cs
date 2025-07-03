using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Data.Inputs
{
 public record BookInput(
    string Title,
    int    Year,
    int   AuthorId  
);

}