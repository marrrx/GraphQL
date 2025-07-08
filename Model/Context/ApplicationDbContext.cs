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
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoanBook> LoanBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder
                .Entity<Nationality>()
                .HasMany(n => n.Authors)
                .WithOne(a => a.Nationality)
                .HasForeignKey(a => a.NationalityId);

            modelBuilder
                .Entity<Loan>()
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId);

            //RELATION N:N BETWEEN TABLE LOANS AND TABLE BOOKS IN THE TABLE LOANBOOKS
            modelBuilder.Entity<LoanBook>().HasKey(l => new { l.LoanId, l.BookId });
            modelBuilder
                .Entity<LoanBook>()
                .HasOne(x => x.Loan)
                .WithMany(b => b.LoanBooks)
                .HasForeignKey(l => l.LoanId);
            modelBuilder
                .Entity<LoanBook>()
                .HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId);

            modelBuilder
                .Entity<Nationality>()
                .HasData(
                    new Nationality
                    {
                        Id = 1,
                        Country = "México",
                        Pronunciation = "Mexicana",
                    },
                    new Nationality
                    {
                        Id = 2,
                        Country = "Chile",
                        Pronunciation = "Chilena",
                    }
                );

            modelBuilder
                .Entity<Author>()
                .HasData(
                    new Author
                    {
                        Id = 1,
                        Name = "Gabriel García Márquez",
                        NationalityId = 1,
                    },
                    new Author
                    {
                        Id = 2,
                        Name = "Isabel Allende",
                        NationalityId = 2,
                    }
                );

            modelBuilder
                .Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = 1,
                        Title = "Cien años de soledad",
                        Year = 1967,
                        AuthorId = 1,
                    },
                    new Book
                    {
                        Id = 2,
                        Title = "El amor en los tiempos del cólera",
                        Year = 1985,
                        AuthorId = 1,
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "La casa de los espíritus",
                        Year = 1982,
                        AuthorId = 2,
                    }
                );
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        Name = "John Doe",
                        PhoneNumber = "4652093011",
                        RegistrationNumber = 001,
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Jane Doe",
                        PhoneNumber = "4652093011",
                        RegistrationNumber = 002,
                    }
                );

            modelBuilder
                .Entity<Loan>()
                .HasData(
                    new Loan
                    {
                        Id = 1,
                        UserId = 1,
                        LoanDate = new DateTime(2023, 1, 1),
                        ReturnDate = new DateTime(2023, 4, 1),
                    },
                    new Loan
                    {
                        Id = 2,
                        UserId = 2,
                        LoanDate = new DateTime(2023, 2, 15),
                        ReturnDate = new DateTime(2023, 5, 15),
                    }
                );

            modelBuilder
                .Entity<LoanBook>()
                .HasData(
                    new LoanBook { LoanId = 1, BookId = 1 },
                    new LoanBook { LoanId = 1, BookId = 2 },
                    new LoanBook { LoanId = 1, BookId = 3 },
                    new LoanBook { LoanId = 2, BookId = 1 },
                    new LoanBook { LoanId = 2, BookId = 2 }
                );
        }
    }
}
