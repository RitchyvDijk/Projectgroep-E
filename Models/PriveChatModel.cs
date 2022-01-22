using System.ComponentModel.DataAnnotations;

public class PriveChat {
    [Key]
    public int Id {get; set;}
    [Required(ErrorMessage = "Er is iets fout gegaan met de datum.")]
    public string DateTime { get; set; }
    [Required(ErrorMessage = "Er is geen ontvanger gevonden.")]
    public int Ontvanger {get; set;}
    [Required(ErrorMessage = "Er is iets fout gegaan met uw identiteit te achterhalen.")]
    public int Afzender {get; set;}
    [Required(ErrorMessage = "Geen tekst gevonden!")]
    public string Body {get; set;}

}