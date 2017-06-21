using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    public class ReserverModel
    {
        [Key]
        [Display(Name = "ReserverID")]
        public int ReserverID { get; set; }

        [Display(Name = "NomSortie")]
        public string NomSortie  { get; set; }

        [Display(Name = "DescriptionSortie")]
        public string DescriptionSortie { get; set; }

        [Display(Name = "SortieID")]
        public int SortieID { get; set; }

        [Display(Name = "NbrEnfants")]
        public int NbrEnfants { get; set; }

        [Display(Name = "NbrAdultes")]
        public int NbrAdultes { get; set; }

        [Display(Name = "NbrBebes")]
        public int NbrBebes { get; set; }

       
    }
}