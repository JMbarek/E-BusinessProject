
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

    public class ReservationValide
    {
        #region Proprietes
        [Key]
        [Display(Name = "ReservationValideID")]
        public int ReservationValideID { get; set; }

        public int ReservationID { get; set; }
        [ForeignKey("ReservationID")]
        public virtual Reservation Reservation { get; set; }

        [Required]
        [Display(Name = "DateValidation")]
        public DateTime DateValidation { get; set; }


        #endregion
    }
}