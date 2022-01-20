using System.ComponentModel.DataAnnotations;

public class Client {
    [Key]
    public int Id {get; set;}
    [Required(ErrorMessage = "Geen gebruikersnaam gevonden.")]
    public string Nickname {get; set;}
    [Required(ErrorMessage = "geen leeftijdscategorie gevoden.")]
    public string Leeftijdscategorie {get; set;}
    [Required(ErrorMessage = "Geen naam gevonden.")]
    public string Naam {get; set;}
    //public Hulpverlener Hulpverlener {get; set;}
    public int HulpverlenerId {get; set;}
    [Required(ErrorMessage = "Geen wachtwoord gevonden.")]
    public string Wachtwoord {get; set;}
}