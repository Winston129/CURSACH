using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cursach.Data;
using cursach.Models;

namespace cursach.Controllers
{
    public class ReservedsController : Controller
    {
        private readonly CursachClientAddItemContext _context;

        public ReservedsController(CursachClientAddItemContext context)
        {
            _context = context;
        }

        // GET: Reserveds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserveds.ToListAsync());
        }

        public async Task<IActionResult> SelectName()
        {
            return View();
        }

        // GET: Reserveds/Details/5
        [HttpGet("Reserveds/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserved = await _context.Reserveds
                .FirstOrDefaultAsync(m => m.ReservedId == id);
            if (reserved == null)
            {
                return NotFound();
            }

            return View(reserved);
        }

        // GET: Reserveds/Details?
        [HttpGet("Reserveds/Details")]
        public async Task<IActionResult> Details(DateOnly reservedDate)
        {
            //if (reservedDate != DateOnly.MinValue)
            //{
            //    return NotFound("Reserved Date is required.");
            //}

            var reserved = await _context.Reserveds
                .FirstOrDefaultAsync(m => m.ReservedDate == reservedDate);
            if (reserved == null)
            {
                return NotFound();
            }

            return View(reserved);
        }

        // GET: Reserveds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserveds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservedId,ReservedDate,ExpirationDate,InterestRate")] Reserved reserved)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserved);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserved);
        }

        // GET: Reserveds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserved = await _context.Reserveds.FindAsync(id);
            if (reserved == null)
            {
                return NotFound();
            }
            return View(reserved);
        }

        // POST: Reserveds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservedId,ReservedDate,ExpirationDate,InterestRate")] Reserved reserved)
        {
            if (id != reserved.ReservedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservedExists(reserved.ReservedId))
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
            return View(reserved);
        }

        // GET: Reserveds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserved = await _context.Reserveds
                .FirstOrDefaultAsync(m => m.ReservedId == id);
            if (reserved == null)
            {
                return NotFound();
            }

            return View(reserved);
        }

        // POST: Reserveds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserved = await _context.Reserveds.FindAsync(id);
            if (reserved != null)
            {
                _context.Reserveds.Remove(reserved);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservedExists(int id)
        {
            return _context.Reserveds.Any(e => e.ReservedId == id);
        }
    }
}
