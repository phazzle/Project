using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class OrderTypesController : Controller
    {
        private readonly WebContext _context;

        public OrderTypesController(WebContext context)
        {
            _context = context;
        }

        // GET: OrderTypes
        public async Task<IActionResult> Index()
        {
              return _context.OrderType != null ? 
                          View(await _context.OrderType.ToListAsync()) :
                          Problem("Entity set 'WebContext.OrderType'  is null.");
        }

        // GET: OrderTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderType == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderType
                .FirstOrDefaultAsync(m => m.OrderTypeId == id);
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // GET: OrderTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderTypeId,Name")] OrderType orderType)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(orderType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(orderType);
        }

        // GET: OrderTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderType == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderType.FindAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }
            return View(orderType);
        }

        // POST: OrderTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderTypeId,Name")] OrderType orderType)
        {
            if (id != orderType.OrderTypeId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(orderType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderTypeExists(orderType.OrderTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(orderType);
        }

        // GET: OrderTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderType == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderType
                .FirstOrDefaultAsync(m => m.OrderTypeId == id);
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // POST: OrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderType == null)
            {
                return Problem("Entity set 'WebContext.OrderType'  is null.");
            }
            var orderType = await _context.OrderType.FindAsync(id);
            if (orderType != null)
            {
                _context.OrderType.Remove(orderType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderTypeExists(int id)
        {
          return (_context.OrderType?.Any(e => e.OrderTypeId == id)).GetValueOrDefault();
        }
    }
}
