using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skolinlämning.Data;
using Skolinlämning.Models;
using System.Reflection.Metadata;

namespace Skolinlämning.Controllers
{
    [Authorize]
    public class BloggController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloggController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bloggs = await _context.Bloggs.ToListAsync();
            return View(bloggs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Content")] BloggPost blogg)
        {
            if (ModelState.IsValid)
            {
                blogg.Author = User.Identity.Name; // Sätt författaren till den inloggade användarens namn
                _context.Add(blogg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogg);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogg = await _context.Bloggs.FindAsync(id);
            if (blogg == null)
            {
                return NotFound();
            }

            // Kontrollera om användaren är ägaren till blogginlägget
            if (blogg.Author != User.Identity.Name)
            {
                return Unauthorized();
            }

            return View(blogg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Content")] BloggPost blogg)
        {
            if (id != blogg.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Kontrollera om användaren är ägaren till blogginlägget
                    var existingBlogg = await _context.Bloggs.FirstOrDefaultAsync(b => b.ID == id && b.Author == User.Identity.Name);
                    if (existingBlogg == null)
                    {
                        return Unauthorized();
                    }

                    existingBlogg.Title = blogg.Title;
                    existingBlogg.Content = blogg.Content;

                    _context.Update(existingBlogg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blogg.ID))
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
            return View(blogg);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogg = await _context.Bloggs.FirstOrDefaultAsync(m => m.ID == id);
            if (blogg == null)
            {
                return NotFound();
            }

            // Kontrollera om användaren är ägaren till blogginlägget eller en administratör
            if (blogg.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            return View(blogg);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogg = await _context.Bloggs.FindAsync(id);

            // Kontrollera om användaren är ägaren till blogginlägget eller en administratör
            if (blogg.Author != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            if (blogg != null)
            {
                _context.Bloggs.Remove(blogg);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Bloggs.Any(e => e.ID == id);
        }
    }
}
