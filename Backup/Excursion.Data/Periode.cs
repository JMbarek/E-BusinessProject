
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

    public class Periode
    {
        #region Proprietes
        [Key]
        [Display(Name = "PeriodeID")]
        public int PeriodeID { get; set; }

        [Required]
        [Display(Name = "DateDebut")]
        public DateTime DateDebut { get; set; }

        [Required]
        [Display(Name = "DateFin")]
        public DateTime DateFin { get; set; }


        #endregion
    }
}
