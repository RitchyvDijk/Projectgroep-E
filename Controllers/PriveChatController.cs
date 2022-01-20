using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace webapplication.Controllers
{
    public class PriveChatController : Controller
    {
        private readonly MyContext _context;

        public PriveChatController(MyContext context)
        {
            _context = context;
        }

        // GET: PriveChat
        public async Task<IActionResult> Index()
        {
            return View(await _context.PriveChat.ToListAsync());
        }

        // GET: PriveChat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priveChat = await _context.PriveChat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priveChat == null)
            {
                return NotFound();
            }

            return View(priveChat);
        }

        // GET: PriveChat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriveChat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,Ontvanger,Afzender,Body")] PriveChat priveChat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priveChat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priveChat);
        }

        // GET: PriveChat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priveChat = await _context.PriveChat.FindAsync(id);
            if (priveChat == null)
            {
                return NotFound();
            }
            return View(priveChat);
        }

        // POST: PriveChat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,Ontvanger,Afzender")] PriveChat priveChat)
        {
            if (id != priveChat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priveChat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriveChatExists(priveChat.Id))
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
            return View(priveChat);
        }

        // GET: PriveChat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priveChat = await _context.PriveChat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priveChat == null)
            {
                return NotFound();
            }

            return View(priveChat);
        }

        // POST: PriveChat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priveChat = await _context.PriveChat.FindAsync(id);
            _context.PriveChat.Remove(priveChat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriveChatExists(int id)
        {
            return _context.PriveChat.Any(e => e.Id == id);
        }
    }
}
