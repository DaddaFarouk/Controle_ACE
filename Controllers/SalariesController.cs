using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Controle_ACE_2.Data;
using Controle_ACE_2.Models;

namespace Controle_ACE_2.Controllers
{
    public class SalariesController : Controller
    {
        private readonly DbSalarieContext _context;

        public SalariesController(DbSalarieContext context)
        {
            _context = context;
        }

        // GET: Salaries
        public async Task<IActionResult> Index()
        {
            var dbSalarieContext = _context.Salarie.Include(s => s.Departement);
            return View(await dbSalarieContext.ToListAsync());
        }

        // GET: Salaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salarie
                .Include(s => s.Departement)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // GET: Salaries/Create
        public IActionResult Create()
        {
            ViewData["DepartementId"] = new SelectList(_context.Departement, "DepartementId", "DepartementId");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Prenom,Fonction,Salaire,DepartementId")] Salarie salarie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salarie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartementId"] = new SelectList(_context.Departement, "DepartementId", "DepartementId", salarie.DepartementId);
            return View(salarie);
        }

        // GET: Salaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salarie.FindAsync(id);
            if (salarie == null)
            {
                return NotFound();
            }
            ViewData["DepartementId"] = new SelectList(_context.Departement, "DepartementId", "DepartementId", salarie.DepartementId);
            return View(salarie);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Prenom,Fonction,Salaire,DepartementId")] Salarie salarie)
        {
            if (id != salarie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salarie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarieExists(salarie.ID))
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
            ViewData["DepartementId"] = new SelectList(_context.Departement, "DepartementId", "DepartementId", salarie.DepartementId);
            return View(salarie);
        }

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarie = await _context.Salarie
                .Include(s => s.Departement)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (salarie == null)
            {
                return NotFound();
            }

            return View(salarie);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salarie = await _context.Salarie.FindAsync(id);
            _context.Salarie.Remove(salarie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarieExists(int id)
        {
            return _context.Salarie.Any(e => e.ID == id);
        }

        // GET: SalariesByAPI
        public async Task<IActionResult> APIResponse(Int32 id = 2)
        {
            SalariesAPI api = new (_context);
            var response = await api.GetSalarieByDepartment(id);
            return View( response.Value);

        }
    }
}
