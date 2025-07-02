using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Model.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);


            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Gabriel García Márquez", Nationality = "Colombiano" },
                new Author { Id = 2, Name = "Isabel Allende", Nationality = "Chilena" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Cien años de soledad", Year = 1967, AuthorId = 1 },
                new Book { Id = 2, Title = "El amor en los tiempos del cólera", Year = 1985, AuthorId = 1 },
                new Book { Id = 3, Title = "La casa de los espíritus", Year = 1982, AuthorId = 2 }
            );
        }

    }
}