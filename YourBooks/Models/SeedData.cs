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
                        Genre = "Philosopics",
                        ReleaseDate = DateTime.Parse("2018-8-1"),
                        Rating = "OK",
                        ImgURL = "https://prodimage.images-bn.com/pimages/9781501196348_p0_v5_s550x406.jpg",
                        WebURL = "https://www.barnesandnoble.com/w/be-fearless-jean-case/1129622111?ean=9781501196348#/",
                        UserId = "952c09f6-ba56-407b-9dc8-76830f5aa916",
                    },
                    new Book
                    {
                        Title = "Becoming",
                        Author = "Michelle Obama",
                        ISBN = 2684915,
                        Genre = "Inspiring",
                        ReleaseDate = DateTime.Parse("2018-11-13"),
                        Rating = "The Best",
                        ImgURL = "https://prodimage.images-bn.com/pimages/9781524763138_p0_v6_s550x406.jpg",
                        WebURL= "https://www.barnesandnoble.com/w/becoming-michelle-obama/1128038172?ean=9781524763138#/",
                        UserId = "952c09f6-ba56-407b-9dc8-76830f5aa916"
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
