using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class BlogEntry
    {
        [Key]
        public int BlogEntryID { get; set; }
        [Required]
        public int BlogID { get; set; }
        [Required]
        public string Title { get; set; } = "<untitled entry>";
        [Required]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
