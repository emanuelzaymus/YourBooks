using Microsoft.AspNetCore.Identity;

namespace YourBooks.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string RemoveMeLater { get; set; }
    }
}
