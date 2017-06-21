using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    public class DayReservationModel
    {
       

        [Display(Name = "Date")]
        public DateTime Date { get; set; }


    }
}