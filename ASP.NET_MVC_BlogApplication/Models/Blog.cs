using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }
        [Required]
        public string OwnerID { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = "<untitled blog>";
    }
}
