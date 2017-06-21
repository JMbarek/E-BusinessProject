using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Excursion.Data
{
    public class ExcursionPL
    {
        [Required]
        [Display(Name = "excursion")]
        public Excursionn excursion { get; set; }
    }
}