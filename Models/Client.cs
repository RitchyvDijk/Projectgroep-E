using System;
using System.ComponentModel.DataAnnotations;

public class Client : Gebruiker
{
    public string Nicknaam { get; set; }
    public Hulpverlener hulpverlener { get; set; }
    public int GebJaar { get; set; }
    [Range(0, 999999999)]
    public int BSN { get; set; }
    public string Probleem { get; set; }

    public Client()
    {
        Nicknaam = GenNickname();
    }

    public string LeeftijdsGroep()
    {
        int leeftijd = DateTime.Now.Year - GebJaar;

        if (leeftijd < 12) return "< 12 jaar";
        else if (leeftijd >= 12 && leeftijd < 16) return "12 tot 16 jaar";
        else return "> 16 jaar";
    }

    public string GenNickname()
    {
        String[] beschrijving = new String[] { "Mooie", "Vieze", "Grootte", "Lichte", "Zwaare", "Rode", "Roze", "Gele", "Blauwe", "Groene", "Lieve", "Coole", "Stoere" };
        String[] dier = new String[] { "Aap", "Kat", "Varken", "Koe", "Hond", "Vis", "Poema", "Tijger", "Leeuw", "Hert", "Beer", "Ezel", "Paard", "Zwaan", "Duif", "Slang", "Olifant" };

        Random rnd = new Random();
        int x, y = -1;

        x = rnd.Next(0, beschrijving.Length);
        y = rnd.Next(0, dier.Length);

        return beschrijving[x] + " " + dier[y];
    }
}
