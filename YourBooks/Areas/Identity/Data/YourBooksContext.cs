using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace YourBooks.Models
{
    public class YourBooksContext : IdentityDbContext<ApplicationUser>
    {
        public YourBooksContext(DbContextOptions<YourBooksContext> options) : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .HasOne(x => x.User)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.UserId);
        }
    }
}
