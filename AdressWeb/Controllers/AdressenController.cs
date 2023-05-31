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
    public class AdressenController : Controller
    {
        private readonly CS2023KursContext _context;

        public AdressenController(CS2023KursContext context)
        {
            _context = context;
        }

        // GET: Adressen
        public async Task<IActionResult> Index()
        {
            var cS2023KursContext = _context.Adressen.Include(a => a.PlzFkNavigation);
            return View(await cS2023KursContext.ToListAsync());
        }

        // GET: Adressen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adressen == null)
            {
                return NotFound();
            }

            var adressen = await _context.Adressen
                .Include(a => a.PlzFkNavigation)
                .FirstOrDefaultAsync(m => m.AdrId == id);
            if (adressen == null)
            {
                return NotFound();
            }

            return View(adressen);
        }

        // GET: Adressen/Create
        public IActionResult Create()
        {
            ViewData["PlzFk"] = new SelectList(_context.Postleitzahlen, "PlzId", "PlzCombo");
            return View();
        }

        // POST: Adressen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdrId,Name,Adresse,Telefon,PlzFk")] Adressen adressen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adressen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlzFk"] = new SelectList(_context.Postleitzahlen, "PlzId", "PlzCombo", adressen.PlzFk);
            return View(adressen);
        }

        // GET: Adressen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adressen == null)
            {
                return NotFound();
            }

            var adressen = await _context.Adressen.FindAsync(id);
            if (adressen == null)
            {
                return NotFound();
            }
            ViewData["PlzFk"] = new SelectList(_context.Postleitzahlen, "PlzId", "PlzCombo", adressen.PlzFk);
            return View(adressen);
        }

        // POST: Adressen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdrId,Name,Adresse,Telefon,PlzFk")] Adressen adressen)
        {
            if (id != adressen.AdrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adressen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdressenExists(adressen.AdrId))
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
            ViewData["PlzFk"] = new SelectList(_context.Postleitzahlen, "PlzId", "PlzCombo", adressen.PlzFk);
            return View(adressen);
        }

        // GET: Adressen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adressen == null)
            {
                return NotFound();
            }

            var adressen = await _context.Adressen
                .Include(a => a.PlzFkNavigation)
                .FirstOrDefaultAsync(m => m.AdrId == id);
            if (adressen == null)
            {
                return NotFound();
            }

            return View(adressen);
        }

        // POST: Adressen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adressen == null)
            {
                return Problem("Entity set 'CS2023KursContext.Adressen'  is null.");
            }
            var adressen = await _context.Adressen.FindAsync(id);
            if (adressen != null)
            {
                _context.Adressen.Remove(adressen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdressenExists(int id)
        {
          return (_context.Adressen?.Any(e => e.AdrId == id)).GetValueOrDefault();
        }
    }
}
