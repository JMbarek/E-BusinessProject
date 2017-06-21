using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    public class Prix
    {
        [Key]
        [Display(Name = "PrixID")]
        public int PrixID { get; set; }


        [Display(Name = "DateDebutP")]
        public DateTime DateDebutP { get; set; }


        [Display(Name = "DateFinP")]
        public DateTime DateFinP { get; set; }

        [Required]
        [Display(Name = "PrixAdulte")]
        public Single PrixAdulte { get; set; }

        [Required]
        [Display(Name = "PrixEnfant")]
        public Single PrixEnfant { get; set; }

        [Required]
        [Display(Name = "PrixBebe")]
        public Single PrixBebe { get; set; }

    } 
}