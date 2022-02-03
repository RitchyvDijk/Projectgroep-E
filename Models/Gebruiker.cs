using Microsoft.AspNetCore.Identity;

public class Gebruiker : IdentityUser
{
    public string VNaam { get; set; }
    public string ANaam { get; set; }

    public string VolledigeNaam()
    {
        return VNaam + " " + ANaam;
    }
}
