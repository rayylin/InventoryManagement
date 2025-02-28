using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    public class SalesPerformanceDailyController : Controller
    {
        private readonly TransactionDbContext _context;

        public SalesPerformanceDailyController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: SalesPerformanceDaily
        public async Task<IActionResult> Index()
        {
              return _context.SalesPerformanceDaily != null ? 
                          View(await _context.SalesPerformanceDaily.ToListAsync()) :
                          Problem("Entity set 'TransactionDbContext.SalesPerformanceDaily'  is null.");
        }

        // GET: SalesPerformanceDaily/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SalesPerformanceDaily == null)
            {
                return NotFound();
            }

            var salesPerformanceDaily = await _context.SalesPerformanceDaily
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (salesPerformanceDaily == null)
            {
                return NotFound();
            }

            return View(salesPerformanceDaily);
        }

        // GET: SalesPerformanceDaily/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesPerformanceDaily/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,ProductId,StoreId,Quantity,Date,Status,CreateTime,CreateUser,UpdateTime,UpdateUser")] SalesPerformanceDaily salesPerformanceDaily)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesPerformanceDaily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesPerformanceDaily);
        }

        // GET: SalesPerformanceDaily/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SalesPerformanceDaily == null)
            {
                return NotFound();
            }

            var salesPerformanceDaily = await _context.SalesPerformanceDaily.FindAsync(id);
            if (salesPerformanceDaily == null)
            {
                return NotFound();
            }
            return View(salesPerformanceDaily);
        }

        // POST: SalesPerformanceDaily/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Guid,ProductId,StoreId,Quantity,Date,Status,CreateTime,CreateUser,UpdateTime,UpdateUser")] SalesPerformanceDaily salesPerformanceDaily)
        {
            if (id != salesPerformanceDaily.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPerformanceDaily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPerformanceDailyExists(salesPerformanceDaily.Guid))
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
            return View(salesPerformanceDaily);
        }

        // GET: SalesPerformanceDaily/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SalesPerformanceDaily == null)
            {
                return NotFound();
            }

            var salesPerformanceDaily = await _context.SalesPerformanceDaily
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (salesPerformanceDaily == null)
            {
                return NotFound();
            }

            return View(salesPerformanceDaily);
        }

        // POST: SalesPerformanceDaily/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SalesPerformanceDaily == null)
            {
                return Problem("Entity set 'TransactionDbContext.SalesPerformanceDaily'  is null.");
            }
            var salesPerformanceDaily = await _context.SalesPerformanceDaily.FindAsync(id);
            if (salesPerformanceDaily != null)
            {
                _context.SalesPerformanceDaily.Remove(salesPerformanceDaily);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesPerformanceDailyExists(string id)
        {
          return (_context.SalesPerformanceDaily?.Any(e => e.Guid == id)).GetValueOrDefault();
        }
    }
}
