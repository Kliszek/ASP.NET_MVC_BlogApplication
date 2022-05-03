using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class Blog
    {
        [Key]
        public string BlogID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string OwnerID { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = "<untitled blog>";
    }
}
