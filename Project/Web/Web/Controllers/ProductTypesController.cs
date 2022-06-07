using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Models
{
    public class ProductTypesController : Controller
    {
        private readonly WebContext _context;

        public ProductTypesController(WebContext context)
        {
            _context = context;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
              return _context.ProductType != null ? 
                          View(await _context.ProductType.ToListAsync()) :
                          Problem("Entity set 'WebContext.ProductType'  is null.");
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductType == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .FirstOrDefaultAsync(m => m.ProductTypeId == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeId,Name")] ProductType productType)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductType == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductTypeId,Name")] ProductType productType)
        {
            if (id != productType.ProductTypeId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeId))
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
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductType == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .FirstOrDefaultAsync(m => m.ProductTypeId == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductType == null)
            {
                return Problem("Entity set 'WebContext.ProductType'  is null.");
            }
            var productType = await _context.ProductType.FindAsync(id);
            if (productType != null)
            {
                _context.ProductType.Remove(productType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
          return (_context.ProductType?.Any(e => e.ProductTypeId == id)).GetValueOrDefault();
        }
    }
}
