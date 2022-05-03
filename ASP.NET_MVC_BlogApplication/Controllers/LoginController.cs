using ASP.NET_MVC_BlogApplication.Data;
using ASP.NET_MVC_BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_BlogApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user)
        {
            if (!_db.Users.Any( u => u.UserName == user.UserName ))
            {
                ModelState.AddModelError("UserName", "User with this username does not exist.");
                return View(user);
            }

            User desiredUser = _db.Users.First(u => u.UserName == user.UserName);

            if (desiredUser.Password != user.Password)
            {
                ModelState.AddModelError("Password", "Provided password is not correct.");
                return View(user);
            }    
                
            TempData["logged"] = "Logged successfully";
            HttpContext.Session.SetString("CurrentUser", $"{desiredUser.UserID}");
            return RedirectToRoute(new { controller = "Blog", action = "Recent" });
        }
    }
}
