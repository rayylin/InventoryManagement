using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement.Controllers
{
    public class SalesPerformanceMonthlyController : Controller
    {
        private readonly TransactionDbContext _context;



        public SalesPerformanceMonthlyController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: SalesPerformanceMonthly
        public async Task<IActionResult> Index(string sortColumn="", string sortDirection = "", string storeId = "", string productId = "")
        {
            //return _context.SalesPerformanceMonthly != null ? 
            //            View(await _context.SalesPerformanceMonthly.ToListAsync()) :
            //            Problem("Entity set 'TransactionDbContext.SalesPerformanceMonthly'  is null.");

            if (string.IsNullOrEmpty(sortColumn)) sortColumn = "Month";
            if (string.IsNullOrEmpty(sortDirection)) sortDirection = "asc";

            ViewData["CurrentSortColumn"] = sortColumn;
            ViewData["CurrentSortDirection"] = sortDirection;

            ViewData["PidSortParam"] = sortColumn == "ProductId" && sortDirection == "asc" ? "desc" : "asc";
            ViewData["StoreIdSortParam"] = sortColumn == "StoreId" && sortDirection == "asc" ? "desc" : "asc";
            ViewData["MonthSortParam"] = sortColumn == "Month" && sortDirection == "asc" ? "desc" : "asc";
            ViewData["YearSortParam"] = sortColumn == "Year" && sortDirection == "asc" ? "desc" : "asc";

            // Fetch Store List for Filtering Dropdown
            var stores = await _context.CustomerPurchase
                .Select(i => i.StoreId)
                .Distinct()
                .ToListAsync();

            var products = await _context.CustomerPurchase
                .Select(i => i.ProductId)
                .Distinct()
                .ToListAsync();

            ViewBag.StoreId = new SelectList(stores);
            ViewBag.ProductId = new SelectList(products);

            // Query Sales Data
            var salesData = _context.SalesPerformanceMonthly.AsQueryable();

            if (!string.IsNullOrEmpty(storeId))
            {
                salesData = salesData.Where(s => s.StoreId == storeId);
            }

            if (!string.IsNullOrEmpty(productId))
            {
                salesData = salesData.Where(s => s.ProductId == productId);
            }

            salesData = sortDirection == "asc"
                ? salesData.OrderBy(e => EF.Property<object>(e, sortColumn))
                : salesData.OrderByDescending(e => EF.Property<object>(e, sortColumn));

            // Query Inventory Data with the same filter
            var inventoryData = _context.Inventory.AsQueryable();

            if (!string.IsNullOrEmpty(storeId))
            {
                inventoryData = inventoryData.Where(i => i.StoreId == storeId);
            }


            var viewModel = new SalesInventoryViewModel
            {
                SalesData = await salesData.ToListAsync(),
                InventoryData = await inventoryData.ToListAsync()
            };

            return View(viewModel);
        }

        // GET: SalesPerformanceMonthly/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SalesPerformanceMonthly == null)
            {
                return NotFound();
            }

            var salesPerformanceMonthly = await _context.SalesPerformanceMonthly
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (salesPerformanceMonthly == null)
            {
                return NotFound();
            }

            return View(salesPerformanceMonthly);
        }

        // GET: SalesPerformanceMonthly/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesPerformanceMonthly/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,ProductId,StoreId,Quantity,Month,Year,Status,CreateTime,CreateUser,UpdateTime,UpdateUser")] SalesPerformanceMonthly salesPerformanceMonthly)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesPerformanceMonthly);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesPerformanceMonthly);
        }

        // GET: SalesPerformanceMonthly/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SalesPerformanceMonthly == null)
            {
                return NotFound();
            }

            var salesPerformanceMonthly = await _context.SalesPerformanceMonthly.FindAsync(id);
            if (salesPerformanceMonthly == null)
            {
                return NotFound();
            }
            return View(salesPerformanceMonthly);
        }

        // POST: SalesPerformanceMonthly/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Guid,ProductId,StoreId,Quantity,Month,Year,Status,CreateTime,CreateUser,UpdateTime,UpdateUser")] SalesPerformanceMonthly salesPerformanceMonthly)
        {
            if (id != salesPerformanceMonthly.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPerformanceMonthly);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPerformanceMonthlyExists(salesPerformanceMonthly.Guid))
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
            return View(salesPerformanceMonthly);
        }

        // GET: SalesPerformanceMonthly/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SalesPerformanceMonthly == null)
            {
                return NotFound();
            }

            var salesPerformanceMonthly = await _context.SalesPerformanceMonthly
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (salesPerformanceMonthly == null)
            {
                return NotFound();
            }

            return View(salesPerformanceMonthly);
        }

        // POST: SalesPerformanceMonthly/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SalesPerformanceMonthly == null)
            {
                return Problem("Entity set 'TransactionDbContext.SalesPerformanceMonthly'  is null.");
            }
            var salesPerformanceMonthly = await _context.SalesPerformanceMonthly.FindAsync(id);
            if (salesPerformanceMonthly != null)
            {
                _context.SalesPerformanceMonthly.Remove(salesPerformanceMonthly);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesPerformanceMonthlyExists(string id)
        {
          return (_context.SalesPerformanceMonthly?.Any(e => e.Guid == id)).GetValueOrDefault();
        }
    }
}
