using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Excursion.Data
{
    public class Themes
    {
        [Required]
        [Display(Name = "themes")]
        public string[] themes { get; set; }
    }

    public class DepartureCountries
    {
        [Required]
        [Display(Name = "departureCountries")]
        public string[] departureCountries { get; set; }
    }

    public class Regions
    {
        [Required]
        [Display(Name = "regions")]
        public string[] regions { get; set; }
    }

    public class DestinationsInRegion
    {
        [Required]
        [Display(Name = "destinationsInRegion")]
        public string[] destinationsInRegion { get; set; }
    }
}