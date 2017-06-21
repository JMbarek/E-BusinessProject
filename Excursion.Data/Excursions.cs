using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Excursion.Data
{
    public class Excursions
    {
        [Required]
        [Display(Name = "excursions")]
        public List<Excursionn> excursions { get; set; }
    }
}