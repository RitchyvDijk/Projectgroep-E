using System.Collections.Generic;

public class Hulpverlener : Gebruiker
{
    public string Specialiteit { get; set; }
    public List<Client> Clienten { get; set; }

    public Hulpverlener()
    {
        Clienten = new List<Client>();
    }
}
