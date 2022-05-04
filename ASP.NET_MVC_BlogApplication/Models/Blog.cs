using ASP.NET_MVC_BlogApplication.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC_BlogApplication.Models
{
    public class Blog
    {
        [Key]
        [Display(Name = "Blog Unique ID")]
        [StringLength(36, MinimumLength = 3)]
        public string BlogID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name = "Owner ID")]
        public string OwnerID { get; set; } = string.Empty;
        [NotMapped]
        public string? OwnerUserName {
            get {
                if(this._ownerUserName == "<unknown>")
                {
                    this._ownerUserName = this.OwnerID == "" ? "<unknown>" : _db.Users.Find(this.OwnerID)!.UserName!;
                }
                return _ownerUserName;
            } }
        private string _ownerUserName = "<unknown>";
        [Required]
        public string Title { get; set; } = "<untitled blog>";

        [NotMapped]
        private readonly ApplicationDbContext _db;
        public Blog(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
