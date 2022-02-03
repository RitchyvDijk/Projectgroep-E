using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using webapplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace webapplication.Controllers
{
    [AllowAnonymous]
    public class AfspraakController : Controller
    {
        private readonly GebruikerDbContext _context;
        private readonly GebruikerDbContext identityDbContext;
        private readonly UserManager<Gebruiker> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AfspraakController(GebruikerDbContext context, GebruikerDbContext identityDbContext, UserManager<Gebruiker> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.identityDbContext = identityDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string pool = "ABCDEFGHIJKLVWXYZmnopqrstuvwxyz0123456789@!@#$%^*$#)(";
            var builder = new System.Text.StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }
        //account maken voor een user
        // [AllowAnonymous]
        // public async Task<IActionResult> maakAcc(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var afspraakModel = await _context.afspraakModel
        //         .FirstOrDefaultAsync(m => m.id == id);
        //     if (afspraakModel == null)
        //     {
        //         return NotFound();
        //     }
        //     //mail van gekozen gebruiker
        //     var mailVanGebruiker = _context.afspraakModel.Select(a => a.emailvanGebruiker);
        //     var gebjaarVanGebruiker = _context.afspraakModel.Select(a => a.geboorteDatum);
        //     var BSNvanGebruiker = _context.afspraakModel.Select(a => a.BSN);

        //     string pw = RandomString(10);
        //     System.Console.WriteLine(pw);

        //     //var nieuweGebruiker = new Client { Nicknaam = mailVanGebruiker, hulpverlener ="", GebJaar = (int)gebjaarVanGebruiker, BSN = BSNvanGebruiker, Probleem = ""};
        //     using (identityDbContext)
        //     {
        //        var result = await userManager.CreateAsync(nieuweGebruiker, pw);
        //         if (!result.Succeeded)
        //     throw new ApplicationException("Unable to create a user.");


        //     return View(afspraakModel);
        // }
        //}


        // GET: Afspraak
        public async Task<IActionResult> Index()
        {
            return View(await _context.afspraakModel.ToListAsync());
        }

        // GET: Afspraak/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraakModel = await _context.afspraakModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (afspraakModel == null)
            {
                return NotFound();
            }

            return View(afspraakModel);
        }

        // GET: Afspraak/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Afspraak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("id,voornaam,achternaam,jongerDan16,geboorteDatum,BSN,naamOuder,emailvanOuder,emailvanGebruiker,gekozenDatum,gekozenTijd,gekozenHulpverlener")] Afspraak afspraakModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afspraakModel);
                await _context.SaveChangesAsync();
                System.Console.WriteLine("Afspraak is succesvol gemaakt");
                return RedirectToAction(nameof(Index));
            }
            return View(afspraakModel);
        }

        // GET: Afspraak/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraakModel = await _context.afspraakModel.FindAsync(id);
            if (afspraakModel == null)
            {
                return NotFound();
            }
            return View(afspraakModel);
        }

        // POST: Afspraak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,voornaam,achternaam,jongerDan16,geboorteDatum,BSN,naamOuder,emailvanOuder,emailvanGebruiker,gekozenDatum,gekozenTijd,gekozenHulpverlener")] Afspraak afspraakModel)
        {
            if (id != afspraakModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(afspraakModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!afspraakModelExists(afspraakModel.id))
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
            return View(afspraakModel);
        }

        // GET: Afspraak/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var afspraakModel = await _context.afspraakModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (afspraakModel == null)
            {
                return NotFound();
            }

            return View(afspraakModel);
        }

        // POST: Afspraak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var afspraakModel = await _context.afspraakModel.FindAsync(id);
            _context.afspraakModel.Remove(afspraakModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool afspraakModelExists(int id)
        {
            return _context.afspraakModel.Any(e => e.id == id);
        }
    }
}
