using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Swap.Data;
using Swap.Models;

namespace Swap.Controllers
{
    public class SwappedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SwappedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Swappeds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Swapped.ToListAsync());
        }

        // GET: Swappeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swapped = await _context.Swapped
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swapped == null)
            {
                return NotFound();
            }

            return View(swapped);
        }

        // GET: Swappeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Swappeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderItemId,ReceiverItemId")] Swapped swapped)
        {
            if (ModelState.IsValid)
            {
                _context.Add(swapped);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(swapped);
        }

        // GET: Swappeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swapped = await _context.Swapped.FindAsync(id);
            if (swapped == null)
            {
                return NotFound();
            }
            return View(swapped);
        }

        // POST: Swappeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderItemId,ReceiverItemId")] Swapped swapped)
        {
            if (id != swapped.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(swapped);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SwappedExists(swapped.Id))
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
            return View(swapped);
        }

        // GET: Swappeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var swapped = await _context.Swapped
                .FirstOrDefaultAsync(m => m.Id == id);
            if (swapped == null)
            {
                return NotFound();
            }

            return View(swapped);
        }

        // POST: Swappeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var swapped = await _context.Swapped.FindAsync(id);
            _context.Swapped.Remove(swapped);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SwappedExists(int id)
        {
            return _context.Swapped.Any(e => e.Id == id);
        }
    }
}
