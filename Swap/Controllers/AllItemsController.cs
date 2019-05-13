using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Swap.Data;
using Swap.Models;
using Swap.Models.ViewModels;

namespace Swap.Controllers
{
    [Authorize]
    public class AllItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;


        public AllItemsController(ApplicationDbContext context,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: AllItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Item
                .Include(i => i.User)
                .Include(i => i.swappeds);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AllItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: AllItems/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: AllItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Img,Description,UserId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", item.UserId);
            return View(item);
        }

        // GET: AllItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", item.UserId);
            return View(item);
        }

        // POST: AllItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Img,Description,UserId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", item.UserId);
            return View(item);
        }

        // GET: AllItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: AllItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

        // GET: AllItems/CreateSwap
        public async Task<IActionResult> CreateSwap(int id)
        {          
            var user = await GetCurrentUserAsync();      

            List<SelectListItem> ItemsList = new List<SelectListItem>();

            var CurrentusersItems = _context.Item
                .Where(s => s.UserId == user.Id);

            ItemsList.Insert(0, new SelectListItem
            {
                Text = "Select",
                Value = ""
            });

            foreach (var pt in CurrentusersItems)
            {
                SelectListItem li = new SelectListItem
                {
                    Value = pt.Id.ToString(),
                    Text = pt.Category
                };
                ItemsList.Add(li);
            };

            SwapViewModel SwapOffer = new SwapViewModel();

            SwapOffer.ItemsSelectList = ItemsList;

            SwapOffer.ReceiverItemId = id;

            return View(SwapOffer);

        }

   

        // POST: AllItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSwap([Bind("Id,Category,Img,Description,UserId,SwappedId, SenderItemId, ReceiverItemId")]int ReceiverItemId, int SenderItemId)
        {

            Swapped swapped = new Swapped();

            var user = await GetCurrentUserAsync();


            var swaps = _context.Item
                .Where(i => i.UserId == user.Id);


            if (ModelState.IsValid)
            {
                swapped.SenderItemId = SenderItemId;    
                swapped.ReceiverItemId = ReceiverItemId;
                _context.Add(swapped);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(swaps, "Id", "Id");
            return View();
        }
    }
}
