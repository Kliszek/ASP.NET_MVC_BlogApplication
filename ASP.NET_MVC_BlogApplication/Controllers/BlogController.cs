using ASP.NET_MVC_BlogApplication.Data;
using ASP.NET_MVC_BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_BlogApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BlogController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recent(string? id)
        {
            if(id != null)
            {
                ViewData["DisplayedBlogId"] = id;
                ViewData["DisplayedBlogEntries"] = _db.BlogEntries.All(be => be.BlogID == id);
            }
            IEnumerable<Blog> blogList = _db.Blogs;
            return View(blogList);
        }

        public IActionResult Create()
        {
            return View(new Blog { BlogID = "", Title="", OwnerID = HttpContext.Session.GetString("CurrentUser")! });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (HttpContext.Session.GetString("CurrentUser") != blog.OwnerID)
            {
                ModelState.AddModelError("OwnerID", "Owner IDs don't match! Did you mess up with it?");
                return View(blog);
            }
            if (_db.Blogs.Any(b => b.BlogID == blog.BlogID))
            {
                ModelState.AddModelError("BlogID", "This blog ID already exists.");
            }
            /*if (_db.Blogs.Any(b => b.Title == blog.Title))
            {
                ModelState.AddModelError("Title", "This blog title already taken.");
            }*/
            if (ModelState.IsValid)
            {
                _db.Add(blog);
                _db.SaveChanges();
                return RedirectToAction("Recent");
            }
            return View(blog);
        }
    }
}
