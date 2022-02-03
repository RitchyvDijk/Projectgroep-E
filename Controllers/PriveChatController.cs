using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using webapplication.Areas.Identity.Data;
using webapplication.Models;

namespace webapplication.Controllers
{
    public class PriveChatController : Controller
    {
        private readonly ChatDbContext _ChatContext;
        private readonly UserManager<Gebruiker> _userManager;
        private readonly GebruikerDbContext _GebruikerContext;


        public PriveChatController(ChatDbContext ChatContext, UserManager<Gebruiker> userManager, GebruikerDbContext GebruikerContext)
        {
            _ChatContext = ChatContext;
            _userManager = userManager;
            _GebruikerContext = GebruikerContext;
        }

        // GET: PriveChat
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Hulpverlener"))
            {
                var HulpverlenerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var namen = new List<string>();
                foreach (var item in await _ChatContext.PriveChat.Where(p => p.Afzender == HulpverlenerId || p.Ontvanger == HulpverlenerId).ToListAsync())
                {
                    if (item.Afzender == HulpverlenerId)
                    {
                        if (_GebruikerContext.Users.Where(u => u.Id == item.Ontvanger).Any() == true)
                        {
                            var naam = _GebruikerContext.Users.Where(u => u.Id == item.Ontvanger).FirstOrDefault().Email;
                            namen.Add(naam);
                        }
                    }
                    else
                    {
                        var naam = _GebruikerContext.Users.Where(u => u.Id == item.Afzender).FirstOrDefault().Email;
                        namen.Add(naam);
                    }
                }
                namen.Sort();
                Int32 index = namen.Count - 1;
                while (index > 0)
                {
                    if (namen[index] == namen[index - 1])
                    {
                        if (index < namen.Count - 1)
                            (namen[index], namen[namen.Count - 1]) = (namen[namen.Count - 1], namen[index]);
                        namen.RemoveAt(namen.Count - 1);
                        index--;
                    }
                    else
                        index--;
                }
                ViewData["HulpverlenerNamen"] = true;
                ViewBag.HulpverlenerNamen = namen;
                return View(_ChatContext.PriveChat.Where(p => p.Afzender == HulpverlenerId || p.Ontvanger == HulpverlenerId).ToList());
            }
            if (User.IsInRole("Client"))
            {
                ViewData["Client"] = true;
                var ClientId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var idHulp = _GebruikerContext.Clients.Where(u => u.Id == ClientId).FirstOrDefault().hulpverlenerId;
                ViewData["ClientHulpverlener"] = idHulp;
            }
            return View(await _ChatContext.PriveChat.ToListAsync());
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
                _ChatContext.Add(priveChat);
                await _ChatContext.SaveChangesAsync();
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

            var priveChat = await _ChatContext.PriveChat.FindAsync(id);
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
                    _ChatContext.Update(priveChat);
                    await _ChatContext.SaveChangesAsync();
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

            var priveChat = await _ChatContext.PriveChat
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
            var priveChat = await _ChatContext.PriveChat.FindAsync(id);
            _ChatContext.PriveChat.Remove(priveChat);
            await _ChatContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriveChatExists(int id)
        {
            return _ChatContext.PriveChat.Any(e => e.Id == id);
        }
    }
}
