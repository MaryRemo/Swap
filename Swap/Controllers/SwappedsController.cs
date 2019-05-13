using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Swap.Data;
using Swap.Models;

namespace Swap.Controllers
{
    [Authorize]
    public class SwappedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;


        public SwappedsController(ApplicationDbContext context,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Swappeds
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var offer = await _context.Swapped
                .Include(s => s.SenderItem)
                .Include(s => s.ReceiverItem)
                .Include(s => s.SenderItem.User)
                .Include(s => s.ReceiverItem.User)
                .ToListAsync();

            return View(offer);
        }

        // GET: Swappeds/AcceptOffer

        public async Task<IActionResult> AcceptOffer (int? id)
        {

            var accept = await _context.Swapped 
                .Include(s => s.SenderItem)
                .Include(s => s.ReceiverItem)
                .Include(s => s.SenderItem.User)
                .Include(s => s.ReceiverItem.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(accept);
            
        }

        //Post: Swappeds/AcceptOffer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptOffer([Bind("Id,SenderItem,ReceiverItem,SenderItemId,ReceiverItemId")] int id, Swapped swapped)
        {
            Message message = new Message();

            var user = await GetCurrentUserAsync();
     
                    if (ModelState.IsValid)
                    {
                        swapped.ReceiverItem.Id = swapped.ReceiverItemId;
                        swapped.SenderItem.Id = swapped.SenderItemId;                       
                        swapped.ReceiverItem.UserId = swapped.SenderItem.UserId;
                        swapped.SenderItem.UserId = user.Id;
                        _context.Update(swapped);

                        message.SenderId = user.Id;
                        message.ReceiverId = swapped.ReceiverItem.UserId;
                        message.Text = $"{user.FirstName} has accepted your offer. Contact them at: {user.PhoneNumber}";
                        message.Datetime = DateTime.Now;
                        _context.Add(message);

                        await _context.SaveChangesAsync();

                        var swap = await _context.Swapped.FindAsync(id);
                        _context.Swapped.Remove(swapped);

                        await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                    }
                    return View(swapped);
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
