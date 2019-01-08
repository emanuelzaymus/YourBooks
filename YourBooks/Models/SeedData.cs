using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourBooks.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new YourBooksContext(
                serviceProvider.GetRequiredService<DbContextOptions<YourBooksContext>>()))
            {
                // Look for any movies.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Be Fearless: 5 Principles for a Life of Breakthroughs and Purpose",
                        Author = "Jean Case",
                        ISBN = 654879,
                        ReleaseDate = DateTime.Parse("2018-8-1"),
                        Genre = "Philosopics",
                        Rating = "OK",
                        UserId = "d86ef0e5-ab85-4b47-b977-feb5cd7e97ec"
                    },
                    new Book
                    {
                        Title = "Becoming",
                        Author = "Michelle Obama",
                        ISBN = 2684915,
                        ReleaseDate = DateTime.Parse("2018-11-13"),
                        Genre = "Inspiring",
                        Rating = "The Best",
                        UserId = "d86ef0e5-ab85-4b47-b977-feb5cd7e97ec"
                    },
                    new Book
                    {
                        Title = "The Other Wes Moore: One Name, Two Fates",
                        Author = " Wes Moore, Tavis Smiley (Afterword)",
                        ISBN = 654879,
                        ReleaseDate = DateTime.Parse("2010-4-27"),
                        Genre = "Philosopics",
                        Rating = "OK",
                        UserId = "d86ef0e5-ab85-4b47-b977-feb5cd7e97ec"
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
