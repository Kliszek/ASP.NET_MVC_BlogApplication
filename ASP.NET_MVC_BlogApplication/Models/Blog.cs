using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class Blog
    {
        [Key]
        [Display(Name = "Blog Unique ID")]
        public string BlogID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name = "Owner ID")]
        public string OwnerID { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = "<untitled blog>";
    }
}
