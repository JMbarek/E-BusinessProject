using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    public class Sortie
    {
      
        [Display(Name = "HeureDepart")]
        public Single HeureDepart { get; set; }
        [Display(Name = "DateSortie")]
        public DateTime DateSortie { get; set; }
        public string TypeExc { get; set; }
        public string Excursion { get; set; }
        public string Etat { get; set; }
        public int NbrePlaceVendable { get; set; }
    }
}