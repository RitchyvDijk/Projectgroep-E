using System.ComponentModel.DataAnnotations;

public class Client {
    [Key]
    public int Id {get; set;}
    [Required]
    public string Nickname {get; set;}
    [Required]
    public string Leeftijdscategorie {get; set;}
    [Required]
    public string Naam {get; set;}
    //public Hulpverlener Hulpverlener {get; set;}
    public string Hulpverlener {get; set;}
    [Required]
    public string Wachtwoord {get; set;}
}