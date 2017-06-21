using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    public class SearchExcursionModel
    {

        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Theme")]
        public string Theme { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "VilleDepart")]
        public string VilleDepart { get; set; }

        [Display(Name = "VilleArrive")]
        public string VilleArrive { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "ApartirDe")]
        public DateTime ApartirDe { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Jsqa")]
        public DateTime Jsqa { get; set; }

        [Display(Name = "Nbrjours")]
        public int Nbrjours { get; set; }


    }

    [Serializable]
    public class SearchExcursionModel2
    {

        public string first { get; set; }
//        public string ID { get; set; }

        public string second { get; set; }

        public string Nbrjours { get; set; }

        public string listvilleRegion { get; set; }

        public string VilleArrive { get; set; }

        public string VilleDepart { get; set; }

        public List<string> Themes  { get; set; }
    }
}