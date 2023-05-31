using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdressWeb.Models;

namespace AdressWeb.Controllers
{
    public class PostleitzahlenController : Controller
    {
        private readonly CS2023KursContext _context;

        public PostleitzahlenController(CS2023KursContext context)
        {
            _context = context;
        }

        // GET: Postleitzahlen
        public async Task<IActionResult> Index()
        {
              return _context.Postleitzahlen != null ? 
                          View(await _context.Postleitzahlen.ToListAsync()) :
                          Problem("Entity set 'CS2023KursContext.Postleitzahlen'  is null.");
        }

        // GET: Postleitzahlen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Postleitzahlen == null)
            {
                return NotFound();
            }

            var postleitzahlen = await _context.Postleitzahlen
                .FirstOrDefaultAsync(m => m.PlzId == id);
            if (postleitzahlen == null)
            {
                return NotFound();
            }

            return View(postleitzahlen);
        }

        // GET: Postleitzahlen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postleitzahlen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlzId,Plz,Ort")] Postleitzahlen postleitzahlen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postleitzahlen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postleitzahlen);
        }

        // GET: Postleitzahlen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Postleitzahlen == null)
            {
                return NotFound();
            }

            var postleitzahlen = await _context.Postleitzahlen.FindAsync(id);
            if (postleitzahlen == null)
            {
                return NotFound();
            }
            return View(postleitzahlen);
        }

        // POST: Postleitzahlen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlzId,Plz,Ort")] Postleitzahlen postleitzahlen)
        {
            if (id != postleitzahlen.PlzId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postleitzahlen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostleitzahlenExists(postleitzahlen.PlzId))
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
            return View(postleitzahlen);
        }

        // GET: Postleitzahlen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Postleitzahlen == null)
            {
                return NotFound();
            }

            var postleitzahlen = await _context.Postleitzahlen
                .FirstOrDefaultAsync(m => m.PlzId == id);
            if (postleitzahlen == null)
            {
                return NotFound();
            }

            return View(postleitzahlen);
        }

        // POST: Postleitzahlen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Postleitzahlen == null)
            {
                return Problem("Entity set 'CS2023KursContext.Postleitzahlen'  is null.");
            }
            var postleitzahlen = await _context.Postleitzahlen.FindAsync(id);
            if (postleitzahlen != null)
            {
                _context.Postleitzahlen.Remove(postleitzahlen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostleitzahlenExists(int id)
        {
          return (_context.Postleitzahlen?.Any(e => e.PlzId == id)).GetValueOrDefault();
        }
    }
}
