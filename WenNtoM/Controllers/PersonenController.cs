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
    public class PersonenController : Controller
    {
        private readonly CS2023KursContext _context;

        public PersonenController(CS2023KursContext context)
        {
            _context = context;
        }

        // GET: Personen
        public async Task<IActionResult> Index()
        {
            return _context.Personen != null ?
                        View(await _context.Personen.ToListAsync()) :
                        Problem("Entity set 'CS2023KursContext.Personen'  is null.");
        }

        // GET: Personen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personen == null)
            {
                return NotFound();
            }

            var personen = await _context.Personen
                .FirstOrDefaultAsync(m => m.PersonenId == id);
            if (personen == null)
            {
                return NotFound();
            }

            return View(personen);
        }

        // GET: Personen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonenId,Name")] Personen personen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personen);
        }

        // GET: Personen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personen == null)
            {
                return NotFound();
            }

            var personen = await _context.Personen.FindAsync(id);
            if (personen == null)
            {
                return NotFound();
            }

            // Füllen der Erweiterungen der Personenklasse
            // Alle Interessen als Liste SelectListItem
            // Value => PrimaryKey eines Interesseneintrags
            // Text => Name der Interesse
            personen.Interessen = _context.Interessen.Select(i => new SelectListItem { Value = i.InteressenId.ToString(), Text = i.Interessen1 });

            // Die der Person zugeordneten Interessen
            personen.SelectedIntTags = _context.PerInt.Where(z => z.PerFk == id).Select(z => z.IntFk);
            //personen.SelectedIntTags = (IEnumerable<SelectListItem>)_context.PerInt.Where(z => z.PerFk == id).Select(z => z.IntFk);

            return View(personen);
        }

        // POST: Personen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonenId,Name,Interessen,SelectedIntTags")] Personen personen)
        {
            if (id != personen.PersonenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personen);

                    // in der Zuordnungstabelle werden alle Einträge der Person gelöscht
                    var zuordnung = _context.PerInt.Where(z => z.PerFk == id);
                    // RemoveRange löscht eine Liste von Objekten
                    _context.PerInt.RemoveRange(zuordnung);

                    // Neueintragen der selektieren Elemente
                    //zuordnung = personen.SelectedIntTags.Select(s => new PerInt { PerFk = id, IntFk = s });
                    //_context.PerInt.AddRange(zuordnung);
                    var zuordnungneu = personen.SelectedIntTags.Select(s => new PerInt { PerFk = id, IntFk = s });
                    _context.PerInt.AddRange(zuordnungneu);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonenExists(personen.PersonenId))
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
            return View(personen);
        }

        // GET: Personen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personen == null)
            {
                return NotFound();
            }

            var personen = await _context.Personen
                .FirstOrDefaultAsync(m => m.PersonenId == id);
            if (personen == null)
            {
                return NotFound();
            }

            return View(personen);
        }

        // POST: Personen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personen == null)
            {
                return Problem("Entity set 'CS2023KursContext.Personen'  is null.");
            }
            var personen = await _context.Personen.FindAsync(id);
            if (personen != null)
            {
                _context.Personen.Remove(personen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonenExists(int id)
        {
            return (_context.Personen?.Any(e => e.PersonenId == id)).GetValueOrDefault();
        }
    }
}
