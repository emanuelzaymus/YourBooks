using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace YourBooks.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Book> Books { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
