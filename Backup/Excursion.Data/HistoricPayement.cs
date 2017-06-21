namespace Excursion.Data
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    #endregion

    public class HistoricPayement
    {
        #region Proprietes
        [Key]
        [Display(Name = "HistoricID")]
        public int HistoricID { get; set; }

        [Required]
        [Display(Name = "DatePayement")]
        public DateTime DatePayement { get; set; }

        [Required]
        [Display(Name = "MontantPaye")]
        public Single MontantPaye { get; set; }

        public int ReservationID { get; set; }
        [ForeignKey("ReservationID")]
        public virtual Reservation Reservation { get; set; }


        #endregion
    }
}