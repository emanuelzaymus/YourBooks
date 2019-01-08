using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourBooks.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Author { get; set; }

        [Required]
        public int ISBN { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
        
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:d.M. yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(20)]
        [Required]
        public string Rating { get; set; }

        [Display(Name = "Image URL")]
        [Url]
        public string ImgURL { get; set; }

        [Display(Name = "Online shop URL")]
        [Url]
        public string WebURL { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
