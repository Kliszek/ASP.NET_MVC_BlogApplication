using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
