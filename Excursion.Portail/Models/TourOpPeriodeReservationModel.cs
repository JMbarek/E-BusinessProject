using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Excursion.Portail.Models
{
    public class TourOpPeriodeReservationModel
    {

        [Display(Name = "PeriodeDebut")]
        public DateTime PeriodeDebut { get; set; }

        [Display(Name = "PeriodeFin")]
        public DateTime PeriodeFin { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "TourOp")]
        public string TourOp { get; set; }

    }
}