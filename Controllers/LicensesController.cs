using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LicensesSystem.Data;
using ProgramowanieZaawansowaneLicencje.Models;
using Microsoft.AspNetCore.Authorization;

namespace LicensesSystem.Controllers
{
    public class LicensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LicensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Licenses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.License.Include(l => l.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Licenses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.License
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LicenseId == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // GET: Licenses/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Licenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("LicenseId,Key,Type,EmployeeId")] License license)
        {
            if (ModelState.IsValid)
            {
                _context.Add(license);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", license.EmployeeId);
            return View(license);
        }

        // GET: Licenses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.License.FindAsync(id);
            if (license == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", license.EmployeeId);
            return View(license);
        }

        // POST: Licenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("LicenseId,Key,Type,EmployeeId")] License license)
        {
            if (id != license.LicenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(license);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseExists(license.LicenseId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "EmployeeId", license.EmployeeId);
            return View(license);
        }

        // GET: Licenses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.License
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LicenseId == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var license = await _context.License.FindAsync(id);
            _context.License.Remove(license);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseExists(int id)
        {
            return _context.License.Any(e => e.LicenseId == id);
        }
    }
}
