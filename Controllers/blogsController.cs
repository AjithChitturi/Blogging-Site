using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloggingSite.Data;
using BloggingSite.Models;
using Microsoft.AspNetCore.Authorization;

namespace BloggingSite.Controllers
{
    [Authorize]
    public class blogsController : Controller
    {
        private readonly BloggingSiteContext _context;

        public blogsController(BloggingSiteContext context)
        {
            _context = context;
        }

        // GET: blogs
        public async Task<IActionResult> Index()
        {
            return _context.blog != null ?
                        View(await _context.blog.ToListAsync()) :
                        Problem("Entity set 'BloggingSiteContext.blog'  is null.");
        }

        // GET: blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.blog == null)
            {
                return NotFound();
            }

            var blog = await _context.blog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,BlogType,Content")] blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.blog == null)
            {
                return NotFound();
            }

            var blog = await _context.blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,BlogType,Content")] blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!blogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.blog == null)
            {
                return NotFound();
            }

            var blog = await _context.blog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.blog == null)
            {
                return Problem("Entity set 'BloggingSiteContext.blog'  is null.");
            }
            var blog = await _context.blog.FindAsync(id);
            if (blog != null)
            {
                _context.blog.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool blogExists(int id)
        {
            return (_context.blog?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}