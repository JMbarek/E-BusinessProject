using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    class Hotel
    {
        [Key]
        [Display(Name = "HotelID")]
        public int HotelID { get; set; }


        [Display(Name = "NbrEtoiles")]
        public int NbrEtoiles { get; set; }


        [Display(Name = "Nom")]
        public string Nom { get; set; }

    }
}
