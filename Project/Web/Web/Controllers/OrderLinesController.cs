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
    public class OrderLinesController : Controller
    {
        private readonly WebContext _context;

        public OrderLinesController(WebContext context)
        {
            _context = context;
        }

        // GET: OrderLines
        public async Task<IActionResult> Index()
        {
            var webContext = _context.OrderLine.Include(o => o.OrderHeader).Include(o => o.ProductType);
            return View(await webContext.ToListAsync());
        }

        // GET: OrderLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderLine == null)
            {
                return NotFound();
            }

            var orderLine = await _context.OrderLine
                .Include(o => o.OrderHeader)
                .Include(o => o.ProductType)
                .FirstOrDefaultAsync(m => m.OrderLineId == id);
            if (orderLine == null)
            {
                return NotFound();
            }

            return View(orderLine);
        }

        // GET: OrderLines/Create
        public IActionResult Create(int? id )
        {
            if (id == null || _context.OrderHeader == null)
            {
                return NotFound();
            }

            var orderHeader = _context.OrderHeader.FindAsync(id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            //ViewData["OrderHeaderId"] = new SelectList(_context.OrderHeader, "OrderHeaderId", "OrderHeaderId");
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Name");
            ViewData["OrderHeaderId"] = orderHeader.Result.OrderHeaderId;

            return View();
        }


        // POST: OrderLines/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderLineId,ProductCode,ProductTypeId,CostPrice,SalesPrice,Quantity,OrderHeaderId")] OrderLine orderLine)
        {
            var max_orderLine = _context.OrderLine.DefaultIfEmpty().Max(r => r == null ? 0 : r.OrderLineId);
            orderLine.LineNumber = max_orderLine+1;
            //if (ModelState.IsValid)
            //{
            _context.Add(orderLine);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "OrderHeaders", new {id = orderLine.OrderHeaderId});
            //}
            ViewData["OrderHeaderId"] = new SelectList(_context.OrderHeader, "OrderHeaderId", "OrderHeaderId", orderLine.OrderHeaderId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Name", orderLine.ProductTypeId);
            //return View(orderLine);
            return RedirectToAction("Details", "OrderHeaders", new { id = orderLine.OrderHeaderId });
        }

        // GET: OrderLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderLine == null)
            {
                return NotFound();
            }

            var orderLine = await _context.OrderLine.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }
            ViewData["OrderHeaderId"] = new SelectList(_context.OrderHeader, "OrderHeaderId", "OrderHeaderId", orderLine.OrderHeaderId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Name", orderLine.ProductTypeId);
            return View(orderLine);
        }

        // POST: OrderLines/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderLineId,LineNumber,ProductCode,ProductTypeId,CostPrice,SalesPrice,Quantity,OrderHeaderId")] OrderLine orderLine)
        {
            if (id != orderLine.OrderLineId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(orderLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderLineExists(orderLine.OrderLineId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return RedirectToAction("Details", "OrderHeaders", new { id = orderLine.OrderHeaderId });
            //}
            ViewData["OrderHeaderId"] = new SelectList(_context.OrderHeader, "OrderHeaderId", "OrderHeaderId", orderLine.OrderHeaderId);
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Name", orderLine.ProductTypeId);
            return RedirectToAction("Details", "OrderHeaders", new { id = orderLine.OrderHeaderId });
        }

        // GET: OrderLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderLine == null)
            {
                return NotFound();
            }

            var orderLine = await _context.OrderLine
                .Include(o => o.OrderHeader)
                .Include(o => o.ProductType)
                .FirstOrDefaultAsync(m => m.OrderLineId == id);
            if (orderLine == null)
            {
                return NotFound();
            }

            return View(orderLine);
        }

        // POST: OrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderLine == null)
            {
                return Problem("Entity set 'WebContext.OrderLine'  is null.");
            }
            var orderLine = await _context.OrderLine.FindAsync(id);
            if (orderLine != null)
            {
                _context.OrderLine.Remove(orderLine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "OrderHeaders", new { id = orderLine.OrderHeaderId });
        }

        private bool OrderLineExists(int id)
        {
          return (_context.OrderLine?.Any(e => e.OrderLineId == id)).GetValueOrDefault();
        }
    }
}
