using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Model.Entities;

namespace GraphQL.Data.Exceptions
{
    public class DeleteResult
    {
        public bool Success { get; set; }
        public object DeleteBook { get; set; }
    }
}