using System;
using System.ComponentModel.DataAnnotations;


public class afspraakModel
{
    [Key]
    public int id { get; set; }
    public string voornaam { get; set; }
    public string achternaam { get; set; }
    //jongerDan16 true = emailvanOuder required
    public bool jongerDan16 { get; set; }

    //gegevens voor de API
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime geboorteDatum { get; set; }
    public string BSN { get; set; }
    public string naamOuder { get; set; }
    public string emailvanOuder { get; set; }
    public string emailvanGebruiker { get; set; }
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime gekozenDatum { get; set; }
    //tijd kan 8:00 tot 17:00 zijn in string formaat.
    public string gekozenTijd { get; set; }

    public HulpverlenerModel gekozenHulpverlener { get; set; }
    //hulpverlenerModel -> NaamZorgverlener

}