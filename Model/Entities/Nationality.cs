using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Model.Entities
{
    public class Nationality
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Pronunciation { get; set; }

        public List<Author> Authors { get; set; }

    }
}