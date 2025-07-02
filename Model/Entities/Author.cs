using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Model.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Nationality { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}