using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class User
    {
        [Key]
        [Display(Name = "User Unique ID")]
        [StringLength(36, MinimumLength = 3)]
        public string UserID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name ="Username")]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [NotMapped]
        public string? PasswordR { get; set; }
    }
}
