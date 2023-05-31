using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WenNtoM.Models;

namespace WenNtoM.Controllers
{
    public class InteressenController : Controller
    {
        private readonly CS2023KursContext _context;

        public InteressenController(CS2023KursContext context)
        {
            _context = context;
        }

        // GET: Interessen
        public async Task<IActionResult> Index()
        {
              return _context.Interessen != null ? 
                          View(await _context.Interessen.ToListAsync()) :
                          Problem("Entity set 'CS2023KursContext.Interessen'  is null.");
        }

        // GET: Interessen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Interessen == null)
            {
                return NotFound();
            }

            var interessen = await _context.Interessen
                .FirstOrDefaultAsync(m => m.InteressenId == id);
            if (interessen == null)
            {
                return NotFound();
            }

            return View(interessen);
        }

        // GET: Interessen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Interessen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InteressenId,Interessen1")] Interessen interessen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interessen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interessen);
        }

        // GET: Interessen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Interessen == null)
            {
                return NotFound();
            }

            var interessen = await _context.Interessen.FindAsync(id);
            if (interessen == null)
            {
                return NotFound();
            }
            return View(interessen);
        }

        // POST: Interessen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InteressenId,Interessen1")] Interessen interessen)
        {
            if (id != interessen.InteressenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interessen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InteressenExists(interessen.InteressenId))
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
            return View(interessen);
        }

        // GET: Interessen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Interessen == null)
            {
                return NotFound();
            }

            var interessen = await _context.Interessen
                .FirstOrDefaultAsync(m => m.InteressenId == id);
            if (interessen == null)
            {
                return NotFound();
            }

            return View(interessen);
        }

        // POST: Interessen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Interessen == null)
            {
                return Problem("Entity set 'CS2023KursContext.Interessen'  is null.");
            }
            var interessen = await _context.Interessen.FindAsync(id);
            if (interessen != null)
            {
                _context.Interessen.Remove(interessen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InteressenExists(int id)
        {
          return (_context.Interessen?.Any(e => e.InteressenId == id)).GetValueOrDefault();
        }
    }
}
