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
    public class CustomerPurchaseontroller : Controller
    {
        private readonly TransactionDbContext _context;

        public CustomerPurchaseontroller(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: CustomerPurchaseontroller
        public async Task<IActionResult> Index()
        {
              return _context.CustomerPurchase != null ? 
                          View(await _context.CustomerPurchase.ToListAsync()) :
                          Problem("Entity set 'TransactionDbContext.CustomerPurchase'  is null.");
        }

        // GET: CustomerPurchaseontroller/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.CustomerPurchase == null)
            {
                return NotFound();
            }

            var customerPurchase = await _context.CustomerPurchase
                .FirstOrDefaultAsync(m => m.CusPurchaseId == id);
            if (customerPurchase == null)
            {
                return NotFound();
            }

            return View(customerPurchase);
        }

        // GET: CustomerPurchaseontroller/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerPurchaseontroller/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CusPurchaseId,ProductId,StoreId,Quantity,UnitCost,Status,CreateTime,CreateUser,UpdateTime,UpdateUser")] CustomerPurchase customerPurchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerPurchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerPurchase);
        }

        // GET: CustomerPurchaseontroller/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.CustomerPurchase == null)
            {
                return NotFound();
            }

            var customerPurchase = await _context.CustomerPurchase.FindAsync(id);
            if (customerPurchase == null)
            {
                return NotFound();
            }
            return View(customerPurchase);
        }

        // POST: CustomerPurchaseontroller/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CusPurchaseId,ProductId,StoreId,Quantity,UnitCost,Status,CreateTime,CreateUser,UpdateTime,UpdateUser")] CustomerPurchase customerPurchase)
        {
            if (id != customerPurchase.CusPurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerPurchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerPurchaseExists(customerPurchase.CusPurchaseId))
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
            return View(customerPurchase);
        }

        // GET: CustomerPurchaseontroller/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.CustomerPurchase == null)
            {
                return NotFound();
            }

            var customerPurchase = await _context.CustomerPurchase
                .FirstOrDefaultAsync(m => m.CusPurchaseId == id);
            if (customerPurchase == null)
            {
                return NotFound();
            }

            return View(customerPurchase);
        }

        // POST: CustomerPurchaseontroller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.CustomerPurchase == null)
            {
                return Problem("Entity set 'TransactionDbContext.CustomerPurchase'  is null.");
            }
            var customerPurchase = await _context.CustomerPurchase.FindAsync(id);
            if (customerPurchase != null)
            {
                _context.CustomerPurchase.Remove(customerPurchase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerPurchaseExists(string id)
        {
          return (_context.CustomerPurchase?.Any(e => e.CusPurchaseId == id)).GetValueOrDefault();
        }
    }
}
