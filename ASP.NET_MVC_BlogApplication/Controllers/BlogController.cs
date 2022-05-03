using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_BlogApplication.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recent()
        {
            ViewBag.loggedUserID = HttpContext.Session.GetString("CurrentUser");
            return View();
        }
    }
}
