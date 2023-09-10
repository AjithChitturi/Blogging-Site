using BloggingSite.Data;
using BloggingSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BloggingSite.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly BloggingSiteContext _context;

        public HomeController(BloggingSiteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var blogs = _context.blog.ToList();
            ViewBag.BlogTypes = _context.blog.Select(b => b.BlogType).Distinct().ToList();
            return View(blogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GetFilteredBlogs(string blogType)
        {
            if (string.IsNullOrEmpty(blogType))
            {
                var blogs = _context.blog.ToList();
                return PartialView("_BlogListPartial", blogs);
            }
            else
            {
                var filteredBlogs = _context.blog.Where(b => b.BlogType == blogType).ToList();
                return PartialView("_BlogListPartial", filteredBlogs);
            }
        }
    }
}