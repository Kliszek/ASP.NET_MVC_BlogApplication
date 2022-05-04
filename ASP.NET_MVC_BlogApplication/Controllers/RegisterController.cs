using ASP.NET_MVC_BlogApplication.Data;
using ASP.NET_MVC_BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_BlogApplication.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                return RedirectToRoute(new { controller = "Blog", action = "Recent" });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user)
        {
            if (user.Password != user.PasswordR)
            {
                ModelState.AddModelError("PasswordR", "The passwords do not match.");
            }
            if(_db.Users.Any(u => u.UserID == user.UserID))
            {
                ModelState.AddModelError("UserID", "This user ID already exists.");
            }
            if (_db.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "This username already taken.");
            }
            if (_db.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "This email address is already in use.");
            }
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["registered"] = "Account created successfully";
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(user);
        }
    }
}
