using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace webapplication.Controllers
{
    public class GroupController : Controller
    {
        private readonly ChatDbContext _context;

        public GroupController(ChatDbContext context)
        {
            _context = context;
        }

        // GET: PriveChat
        public async Task<IActionResult> Index(string zoek, string topic, string age)
        {
            if (User.IsInRole("Moderator")) { ViewData["Moderator"] = true; }
            ViewData["topics"] = await _context.Groups.ToListAsync();
            if (zoek != null)
            {
                ViewData["Zoek"] = zoek;
                return View(await Zoek(_context.Groups, zoek).ToListAsync());
            }
            if (topic != null)
            {
                return View(await Topic(_context.Groups, topic).ToListAsync());
            }
            if (age != null)
            {
                return View(await Age(_context.Groups, age).ToListAsync());

            }
            return View(await _context.Groups.ToListAsync());
        }

        public IQueryable<Group> Zoek(IQueryable<Group> lijst, string zoek)
        {
            return lijst.Where(p => p.Titel.ToLower().Contains(zoek.ToLower()));
        }

        public IQueryable<Group> Topic(IQueryable<Group> lijst, string topic)
        {
            return lijst.Where(p => p.Topic.ToLower().Contains(topic.ToLower()));
        }

        public IQueryable<Group> Age(IQueryable<Group> lijst, string age)
        {
            switch (age)
            {
                case "low":
                    return lijst.Where(p => p.Leeftijdscategorie < 16);

                case "middle":
                    return lijst.Where(p => p.Leeftijdscategorie > 15 && p.Leeftijdscategorie < 19);

                default:
                    return lijst.Where(p => p.Leeftijdscategorie > 18);
            }

        }


        // GET: PriveChat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
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
        public async Task<IActionResult> Create([Bind("Id,Titel,Topic,Leeftijdscategory")] Group groups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        // GET: GroupChat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.FindAsync(id);
            if (groups == null)
            {
                return NotFound();
            }
            return View(groups);
        }

        // POST: GroupChat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Topic,Leeftijdscategory")] Group groups)
        {
            if (id != groups.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupChatsExists(groups.Id))
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
            return View(groups);
        }

        // GET: PriveChat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // POST: PriveChat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupChatsExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
