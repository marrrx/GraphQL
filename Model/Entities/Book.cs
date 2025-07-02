using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Model.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public  Author Author { get; set; }
    }
}