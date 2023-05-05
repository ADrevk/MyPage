using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        static List<BlogEntry> Posts = new List<BlogEntry>();
        public IActionResult Index()
        {
            return View("Index", Posts);
        }

        public IActionResult CreatorPage(Guid id)
        {
            if (id != Guid.Empty)
            {
                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == id);

                return View(model: existingEntry);
            }
          
            return View();
        }
        [HttpGet]
        public IActionResult EditPage([FromRoute] Guid id)
        {
            var post = Posts.Find(x => x.Id == id);
            return View("CreatorPage", post);
        }

        [HttpPost]
        public IActionResult CreatorPage(BlogEntry entry)
        {
            if(entry.Id == Guid.Empty)
            {
                BlogEntry newEntry = new BlogEntry();
                newEntry.Content = entry.Content;
                newEntry.Id = Guid.NewGuid();
                Posts.Add(newEntry);
            } else
            {
                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == entry.Id);
                existingEntry.Content = entry.Content;
            }

            return RedirectToAction("Index");
        }

    }
}
