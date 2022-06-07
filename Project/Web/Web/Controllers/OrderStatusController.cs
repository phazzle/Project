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
    public class OrderStatusController : Controller
    {
        private readonly WebContext _context;

        public OrderStatusController(WebContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {
              return _context.OrderStatus != null ? 
                          View(await _context.OrderStatus.ToListAsync()) :
                          Problem("Entity set 'WebContext.OrderStatus'  is null.");
        }

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderStatus == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus
                .FirstOrDefaultAsync(m => m.OrderStatusId == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // GET: OrderStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatusId,Name")] OrderStatus orderStatus)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(orderStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderStatus == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus.FindAsync(id);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return View(orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderStatusId,Name")] OrderStatus orderStatus)
        {
            if (id != orderStatus.OrderStatusId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(orderStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusExists(orderStatus.OrderStatusId))
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
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderStatus == null)
            {
                return NotFound();
            }

            var orderStatus = await _context.OrderStatus
                .FirstOrDefaultAsync(m => m.OrderStatusId == id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderStatus == null)
            {
                return Problem("Entity set 'WebContext.OrderStatus'  is null.");
            }
            var orderStatus = await _context.OrderStatus.FindAsync(id);
            if (orderStatus != null)
            {
                _context.OrderStatus.Remove(orderStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusExists(int id)
        {
          return (_context.OrderStatus?.Any(e => e.OrderStatusId == id)).GetValueOrDefault();
        }
    }
}
