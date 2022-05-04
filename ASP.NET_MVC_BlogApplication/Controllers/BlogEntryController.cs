using ASP.NET_MVC_BlogApplication.Data;
using ASP.NET_MVC_BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_BlogApplication.Controllers
{
    public class BlogEntryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BlogEntryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string? id)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            ViewData["AllBlogs"] = _db.Blogs;

            ViewData["ManagedBlog"] = _db.Blogs.Find(id);
            string ownerId = HttpContext.Session.GetString("CurrentUser")!;
            ViewData["ManagedBlogs"] = _db.Blogs.Where(b => b.OwnerID == ownerId);
            return View(new BlogEntry { BlogID = id!, BlogEntryID = "", Title = "", Content="" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogEntry blogEntry)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                TempData["expired"] = "Your session has expired due to inactivity.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_db.Blogs.Any(b => (b.BlogID == blogEntry.BlogID && b.OwnerID == HttpContext.Session.GetString("CurrentUser"))))
            {
                ModelState.AddModelError("BlogID", "You don't own a blog with such ID.");
            }
            if (_db.BlogEntries.Any(be => be.BlogEntryID == blogEntry.BlogEntryID))
            {
                ModelState.AddModelError("BlogEntryID", "This blog entry ID already exists.");
            }
            /*if (_db.Blogs.Any(b => b.Title == blog.Title))
            {
                ModelState.AddModelError("Title", "This blog title already taken.");
            }*/
            if (ModelState.IsValid)
            {
                _db.Add(blogEntry);
                _db.SaveChanges();
                return RedirectToRoute(new { controller = "Blog", action = "Recent", id = blogEntry.BlogID });
            }

            ViewData["AllBlogs"] = _db.Blogs;
            ViewData["ManagedBlog"] = _db.Blogs.Find(blogEntry.BlogID);
            string ownerId = HttpContext.Session.GetString("CurrentUser")!;
            ViewData["ManagedBlogs"] = _db.Blogs.Where(b => b.OwnerID == ownerId);
            return View(blogEntry);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            BlogEntry? beToRemove = _db.BlogEntries.Find(id);

            if (beToRemove == null)
                return Content("wrongID");

            Blog parentBlog = _db.Blogs.Find(beToRemove.BlogID)!;
            if (parentBlog.OwnerID != HttpContext.Session.GetString("CurrentUser"))
                return Content("wrongUser");

            _db.BlogEntries.Remove(beToRemove);
            _db.SaveChanges();
            return Content("success");
        }

        public IActionResult Edit(string id)
        {
            BlogEntry editedBlog = _db.BlogEntries.Find(id)!;

            ViewData["AllBlogs"] = _db.Blogs;
            return View(editedBlog);
        }

        [HttpPost]
        public IActionResult Edit(BlogEntry blogEntry)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                ModelState.AddModelError("CustomError", "Your session has expired.");
                TempData["expired"] = "Your session has expired due to inactivity.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_db.Blogs.Any(b => (b.BlogID == blogEntry.BlogID && b.OwnerID == HttpContext.Session.GetString("CurrentUser"))))
            {
                ModelState.AddModelError("BlogID", "You don't own a blog with such ID.");
            }
            if (!_db.BlogEntries.Any(be => be.BlogEntryID == blogEntry.BlogEntryID))
            {
                ModelState.AddModelError("BlogEntryID", "This blog entry ID does not exist.");
            }
            /*if (_db.Blogs.Any(b => b.Title == blog.Title))
            {
                ModelState.AddModelError("Title", "This blog title already taken.");
            }*/
            if (ModelState.IsValid)
            {
                BlogEntry beToEdit = _db.BlogEntries.Find(blogEntry.BlogEntryID)!;
                _db.Entry(beToEdit).CurrentValues.SetValues(blogEntry);
                _db.SaveChanges();
                return RedirectToRoute(new { controller = "Blog", action = "Recent", id = blogEntry.BlogID });
            }

            ViewData["AllBlogs"] = _db.Blogs;
            ViewData["ManagedBlog"] = _db.Blogs.Find(blogEntry.BlogID);
            string ownerId = HttpContext.Session.GetString("CurrentUser")!;
            ViewData["ManagedBlogs"] = _db.Blogs.Where(b => b.OwnerID == ownerId);
            return View(blogEntry);
        }
    }
}
