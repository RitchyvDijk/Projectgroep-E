using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using webapplication.Areas.Identity.Data;

[AllowAnonymous]
public class AfspraakMaken : PageModel
{
    private readonly SignInManager<Gebruiker> _signInManager;
    private readonly UserManager<Gebruiker> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly GebruikerDbContext _gebruikerContext;

    public AfspraakMaken(
        UserManager<Gebruiker> userManager,
        SignInManager<Gebruiker> signInManager,
        IEmailSender emailSender,
        GebruikerDbContext gebruikerContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _gebruikerContext = gebruikerContext;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public List<Hulpverlener> Hulpverleners { get; set; }
    public IEnumerable<SelectListItem> HulpverlenerList { get; set; }

    public class InputModel
    {

        // Client gegevens
        [Required]
        [Display(Name = "Voornaam")]
        public string VNaam { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string ANaam { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        public int GebJaar { get; set; }

        [Required]
        [Display(Name = "BSN")]
        public string BSN { get; set; }

        [Required]
        [Display(Name = "Aandoening")]
        public string Aandoening { get; set; }

        [Required]
        [Display(Name = "Hulpverlener")]
        public string _Hulpverlener { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Afspraak gegevens

        [Display(Name = "Naam ouder/voogd")]
        public string NaamOuder { get; set; }

        [Display(Name = "Email ouder/voogd")]
        public string EmailOuder { get; set; }

        [Required]
        [Display(Name = "Datum en tijd van afspraak")]
        public DateTime DatumTijd { get; set; }

    }

    public void OnGetAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
        Hulpverleners = _gebruikerContext.Hulpverleners.ToList();
        HulpverlenerList = new SelectList(Hulpverleners, nameof(Hulpverlener.Id), nameof(Hulpverlener.VNaam));
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            var hulpverlenerGekozen = _gebruikerContext.Hulpverleners.Single(h => h.Id.Equals(Input._Hulpverlener));
            var user = new Client { VNaam = Input.VNaam, ANaam = Input.ANaam, UserName = Input.Email, GebJaar = Input.GebJaar, Email = Input.Email, Probleem = Input.Aandoening, hulpverlener = hulpverlenerGekozen };
            var result = await _userManager.CreateAsync(user);
            await _gebruikerContext.afspraakModel.AddAsync(new Afspraak { client = user, gekozenDatumTijd = Input.DatumTijd, gekozenHulpverlener = hulpverlenerGekozen });
            await _gebruikerContext.SaveChangesAsync();
            if (result.Succeeded)
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Afspraak/AfspraakConfirm",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("AfspraakMakenConfirm", new { email = Input.Email, returnUrl = returnUrl });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        Console.Write("Gaat fout jongen");

        // If we got this far, something failed, redisplay form
        return Page();
    }
}
