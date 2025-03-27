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
    public class InventoryController : Controller
    {
        private readonly TransactionDbContext _context;

        public InventoryController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Inventory
        public async Task<IActionResult> Index(string storeId = "", string needReorder = "")
        {
            Response.Cookies.Append("UserTodo", "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1) // Expire the cookie in the past
            });


            //return _context.Inventory != null ? 
            //            View(await _context.Inventory.ToListAsync()) :
            //            Problem("Entity set 'TransactionDbContext.Inventory'  is null.");

            //var stores = await _context.Inventory
            //                    .Select(i => i.StoreId)
            //                    .Distinct()
            //                    .ToListAsync();

            //ViewBag.StoreId = new SelectList(stores);

            //var inventory = _context.Inventory.AsQueryable();

            //if (!string.IsNullOrEmpty(storeId))
            //{
            //    inventory = inventory.Where(i => i.StoreId == storeId);
            //}

            //return View(await inventory.ToListAsync());

            // Retrieve distinct stores (StoreId & StoreName)
            var stores = await _context.Store
                                       .Select(s => new { s.StoreId, s.StoreName })
                                       .Distinct()
                                       .ToListAsync();

            ViewBag.StoreId = new SelectList(stores, "StoreId", "StoreName");

            // Retrieve inventory with StoreName included
            var inventoryQuery = _context.Inventory
                                         .Include(i => i.Store) // JOIN Store table
                                         .Include(i => i.Products) // Join Product table
                                         .AsQueryable();

            // Filter by StoreId if provided
            if (!string.IsNullOrEmpty(storeId))
            {
                inventoryQuery = inventoryQuery.Where(i => i.StoreId == storeId);
            }

            // Filter by NeedReorder if provided
            if (!string.IsNullOrEmpty(needReorder))
            {
                inventoryQuery = inventoryQuery.Where(i => (i.Quantity < i.SafetyStock ? "Y" : "N") == needReorder);
            }

            inventoryQuery.OrderBy(e => EF.Property<object>(e, "UpdateTime"));

            return View(await inventoryQuery.ToListAsync());

        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventory/Create
        public async Task<IActionResult> Create()
        {
            var stores = await _context.Store
                        .Select(s => new { s.StoreId, s.StoreName })
                        .Distinct()
                        .ToListAsync();

            ViewBag.Stores = new SelectList(stores, "StoreId", "StoreName");

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pending", Text = "Pending" },
                new SelectListItem { Value = "Delivered", Text = "Delivered" },
                new SelectListItem { Value = "In-Transit", Text = "In-Transit" }
            };


            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,StoreId,Quantity,SafetyStock,Status,Comment,UpdateTime,UpdateUser")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,StoreId,Quantity,SafetyStock,Status,Comment,UpdateTime,UpdateUser")] Inventory inventory)
        {
            if (id != inventory.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.ProductId))
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
            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Inventory == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventory
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventory == null)
            {
                return Problem("Entity set 'TransactionDbContext.Inventory'  is null.");
            }
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(string id)
        {
          return (_context.Inventory?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
