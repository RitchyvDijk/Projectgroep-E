using System;
using System.ComponentModel.DataAnnotations;


public class afspraakModel
{
    [Key]
    public int id { get; set; }
    [StringLength(25)]
    public string voornaam { get; set; }
    [StringLength(25)]
    public string achternaam { get; set; }
    //jongerDan16, gegevens van ouders niet required
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    [Required(AllowEmptyStrings = true)]
    public bool jongerDan16 { get; set; }

    //gegevens voor de API
    // [DataType(DataType.DateTime)]
    // [DisplayFormat(DataFormatString = "{0dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime geboorteDatum { get; set; }
    [StringLength(9)]
    public string BSN { get; set; }

    [DisplayFormat(ConvertEmptyStringToNull = false)]
    
    public string? naamOuder { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    
    public string? emailvanOuder { get; set; }
    [StringLength(50)]
    public string emailvanGebruiker { get; set; }
    // [DataType(DataType.DateTime)]
    // [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime gekozenDatum { get; set; }
    //tijd kan 8:00 tot 17:00 zijn in string formaat.
    public string gekozenTijd { get; set; }

    public string gekozenHulpverlener { get; set; }
    //hulpverlenerModel -> NaamZorgverlener

}