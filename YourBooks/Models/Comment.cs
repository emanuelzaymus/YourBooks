using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourBooks.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
