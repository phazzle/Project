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
    public class OrderHeadersController : Controller
    {
        private readonly WebContext _context;

        public OrderHeadersController(WebContext context)
        {
            _context = context;
        }

        // GET: OrderHeaders
        public async Task<IActionResult> Index(string? searchString)
        {
            var query = from x in _context.OrderHeader select x;
            if(!string.IsNullOrEmpty(searchString))
            {
               
                query = query.Where(s=>s.OrderNumber.ToString().Contains(searchString)
                || s.CustomerName.Contains(searchString)
                || s.CreateDate.ToString().Contains(searchString)
                || s.OrderType.Name.Contains(searchString)
                || s.Status.Name.Contains(searchString)
                || s.OrderHeaderId.ToString().Contains(searchString)
                );
            }
            else
            {
               query = _context.OrderHeader.Include(o => o.OrderType).Include(o => o.Status);
            }
            query.Include(o => o.OrderType).Include(o => o.Status);

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id,string? searchString)
        {
            if (id == null || _context.OrderHeader == null)
            {
                return NotFound();
            }
            var orderHeader = await _context.OrderHeader
                             .Include(o => o.OrderType)
                             .Include(o => o.Status)
                             .Include(o => o.OrderLines)
                             .FirstOrDefaultAsync(m => m.OrderHeaderId == id);

            if (!string.IsNullOrEmpty(searchString))
            {
                List<OrderLine> product = _context.OrderLine.Where(p => p.OrderHeaderId == id
                && p.ProductCode.Contains(searchString)).ToList();
                orderHeader.OrderLines = product;


                if (product.Count()==0)
                {

                    List<OrderLine> x = _context.OrderLine.Where(y => y.OrderHeaderId == id
                    && y.ProductType.Name.Contains(searchString)).ToList();
                    orderHeader.OrderLines = x;
                }
            }


            
            


            if (orderHeader == null)
            {
                return NotFound();
            }
    

            return View(orderHeader);
        }

        // GET: OrderHeaders/Create
        public IActionResult Create()
        {
            ViewData["OrderTypeId"] = new SelectList(_context.Set<OrderType>(), "OrderTypeId", "Name");
            ViewData["OrderStatusId"] = new SelectList(_context.Set<OrderStatus>(), "OrderStatusId", "Name");
            return View();
        }

        // POST: OrderHeaders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderHeaderId,OrderNumber,OrderTypeId,OrderStatusId,CustomerName,CreateDate")] OrderHeader orderHeader)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(orderHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["OrderTypeId"] = new SelectList(_context.Set<OrderType>(), "OrderTypeId", "Name", orderHeader.OrderTypeId);
            ViewData["OrderStatusId"] = new SelectList(_context.Set<OrderStatus>(), "OrderStatusId", "Name", orderHeader.OrderStatusId);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderHeader == null)
            {
                return NotFound();
            }

            var orderHeader = await _context.OrderHeader.FindAsync(id);
            if (orderHeader == null)
            {
                return NotFound();
            }
            ViewData["OrderTypeId"] = new SelectList(_context.Set<OrderType>(), "OrderTypeId", "Name", orderHeader.OrderTypeId);
            ViewData["OrderStatusId"] = new SelectList(_context.Set<OrderStatus>(), "OrderStatusId", "Name", orderHeader.OrderStatusId);
            return View(orderHeader);
        }

        // POST: OrderHeaders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderHeaderId,OrderNumber,OrderTypeId,OrderStatusId,CustomerName,CreateDate")] OrderHeader orderHeader)
        {
            if (id != orderHeader.OrderHeaderId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(orderHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderHeaderExists(orderHeader.OrderHeaderId))
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
            ViewData["OrderTypeId"] = new SelectList(_context.Set<OrderType>(), "OrderTypeId", "Name", orderHeader.OrderTypeId);
            ViewData["OrderStatusId"] = new SelectList(_context.Set<OrderStatus>(), "OrderStatusId", "Name", orderHeader.OrderStatusId);
            return View(orderHeader);
        }

        // GET: OrderHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderHeader == null)
            {
                return NotFound();
            }

            var orderHeader = await _context.OrderHeader
                .Include(o => o.OrderType)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrderHeaderId == id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            return View(orderHeader);
        }

        // POST: OrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderHeader == null)
            {
                return Problem("Entity set 'WebContext.OrderHeader'  is null.");
            }
            var orderHeader = await _context.OrderHeader.FindAsync(id);
            if (orderHeader != null)
            {
                var orderLines = _context.OrderLine.Where(m => m.OrderHeaderId == id);
                foreach (var line in orderLines)
                {
                    _context.OrderLine.Remove(line);
                }
                _context.OrderHeader.Remove(orderHeader);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderHeaderExists(int id)
        {
          return (_context.OrderHeader?.Any(e => e.OrderHeaderId == id)).GetValueOrDefault();
        }
    }
}
