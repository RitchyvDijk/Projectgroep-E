using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using webapplication.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

public class RoleController : Controller
{
    RoleManager<IdentityRole> roleManager;
    //dependency injection
    GebruikerDbContext dbContext;
    UserManager<Gebruiker> userManager;
    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Gebruiker> userManager, GebruikerDbContext dbContext)
    {
        this.roleManager = roleManager;
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    // [Authorize(Policy = "readpolicy")]
    public async Task<IActionResult> Index()
    {
        //var roles = roleManager.Roles.ToList();
        //return View(roles);
        return View(await dbContext.Roles.ToListAsync());
    }

    public IActionResult ManageUserRoles()
    {
        //dropdown menu om de rollen te tonen
        var list = dbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        ViewBag.Roles = list;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddRoleToUser(string UserName, string RoleName)
    {

        var user = dbContext.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        userManager.AddToRoleAsync(user, RoleName);

        ViewBag.ResultMessage = "Role created successfully !";

        //dropdown menu om de rollen te tonen
        var list = dbContext.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
        ViewBag.Roles = list;

        return View("ManageUserRoles");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetRoles(string UserName)
    {
        if (!string.IsNullOrWhiteSpace(UserName))
        {
            var user = dbContext.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            ViewBag.RolesForThisUser = userManager.GetRolesAsync(user);

            //dropdown menu om de rollen te tonen
            var list = dbContext.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = list;
        }

        return View("ManageUserRoles");
    }



    // [Authorize(Policy = "writepolicy")]
    public IActionResult Create()
    {
        return View(new IdentityRole());
    }

    [HttpPost]
    // public async Task<IActionResult> Create(IdentityRole role)
    public async Task<IActionResult> Create(IdentityRole role)
    {
        await roleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }
    //
    // GET: /Roles/Edit/5
    // GET: /Roles/Edit/5
    public ActionResult Edit(string roleName)
    {
        var thisRole = roleManager.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        return View(thisRole);
    }


    //
    // POST: /Roles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Microsoft.AspNetCore.Identity.IdentityRole role)
    {
        try
        {
            dbContext.Entry(role).State = EntityState.Modified;
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
}
