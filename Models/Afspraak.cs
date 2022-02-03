using System;
using System.ComponentModel.DataAnnotations;


public class Afspraak
{
    [Key]
    public int id { get; set; }
    public Client client { get; set; }
    public string naamOuder { get; set; }
    public string emailvanOuder { get; set; }
    public DateTime DatumTijd { get; set; }
    public Hulpverlener gekozenHulpverlener { get; set; }
}
