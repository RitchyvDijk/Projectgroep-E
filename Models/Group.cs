using System.ComponentModel.DataAnnotations;

public class Group {
    [Key]
    public int Id {get; set;}
    [Required(ErrorMessage = "Geen naam gevonden")]
    public string Titel { get; set; }
    [Required(ErrorMessage = "Er is geen topic gevonden.")]
    public string Topic {get; set;}
    [Required(ErrorMessage = "Er is geen leeftijdscategorie gevonden")]
    public int Leeftijdscategorie {get; set;}

}