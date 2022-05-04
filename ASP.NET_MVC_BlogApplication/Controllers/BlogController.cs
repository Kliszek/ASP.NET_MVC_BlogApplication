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
            return RedirectToAction("Recent");
        }

        public IActionResult Recent(string? id)
        {
            if(id != null)
            {
                ViewData["DisplayedBlog"] = _db.Blogs.Find(id);
                ViewData["DisplayedBlogEntries"] = _db.BlogEntries.Where(be => be.BlogID == id);
            }
            ViewData["AllBlogs"] = _db.Blogs;
            return View();
        }

        public IActionResult Delete(string? id)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                TempData["expired"] = "Your session has expired due to inactivity.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            ViewData["AllBlogs"] = _db.Blogs;

            string ownerId = HttpContext.Session.GetString("CurrentUser")!;
            ViewData["ManagedBlogs"] = _db.Blogs.Where(b => b.OwnerID == ownerId);
            
            if (id == null)
                return View();
            else
                return View(_db.Blogs.Find(id));
        }

        [HttpPost]
        public IActionResult Delete(Blog blog)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                TempData["expired"] = "Your session has expired due to inactivity.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            IEnumerable<BlogEntry> beToRemove = _db.BlogEntries.Where(be => be.BlogID == blog.BlogID);
            foreach(BlogEntry be in beToRemove)
                _db.BlogEntries.Remove(be);
            _db.Blogs.Remove(blog);
            _db.SaveChanges();

            return RedirectToAction("Recent");
        }

        public IActionResult Create()
        {
            ViewData["AllBlogs"] = _db.Blogs;
            return View(new Blog { BlogID = "", Title="", OwnerID = HttpContext.Session.GetString("CurrentUser")! });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                TempData["expired"] = "Your session has expired due to inactivity.";
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
            ViewData["AllBlogs"] = _db.Blogs;
            return View(blog);
        }
    }
}
