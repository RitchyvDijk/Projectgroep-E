using System;
using System.ComponentModel.DataAnnotations;


public class Afspraak
{
    [Key]
    public int id { get; set; }
    public Client client { get; set; }
    //jongerDan16, gegevens van ouders niet required ** overbodig
    // [DisplayFormat(ConvertEmptyStringToNull = false)]
    // [Required(AllowEmptyStrings = true)]
    // public bool jongerDan16 { get; set; }

    //gegevens voor de API
    // [DataType(DataType.DateTime)]
    // [DisplayFormat(DataFormatString = "{0dd-MM-yyyy}", ApplyFormatInEditMode = true)]


    //geeft warning, maar op deze manier zijn nulls ook toegestaan
    public string naamOuder { get; set; }
    [DisplayFormat(ConvertEmptyStringToNull = false)]

    //geeft warning, maar op deze manier zijn nulls ook toegestaan
    public string emailvanOuder { get; set; }
    // [DataType(DataType.DateTime)]
    // [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime gekozenDatumTijd { get; set; }

    public Hulpverlener gekozenHulpverlener { get; set; }
    //hulpverlenerModel -> NaamZorgverlener

}
