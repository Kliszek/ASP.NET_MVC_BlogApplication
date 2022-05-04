using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public string OwnerUserName { get; set; } = "<unknown>";
        [Required]
        public string Title { get; set; } = "<untitled blog>";
    }
}
