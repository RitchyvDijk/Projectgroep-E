using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class HulpverlenerModel{
    [Key]
    public int id {get;set;}
    public string NaamZorgverlener{get;set;}
    //peter, sacha, nikolette, viktoria
    public DateTime beschikbareDagen {get;set;}

    //tijden in string formaat van 8:00 tot 17:00
    public string beschikbareTijden {get;set;}
    //public List<> emailClients {get;set;}

}