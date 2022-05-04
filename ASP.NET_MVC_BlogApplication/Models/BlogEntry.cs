using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class BlogEntry
    {
        [Key]
        [Display(Name = "Blog Entry Unique ID")]
        [StringLength(36, MinimumLength = 3)]
        public string BlogEntryID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name = "Parent Blog Unique ID")]
        public string BlogID { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = "<untitled entry>";
        [Required]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
