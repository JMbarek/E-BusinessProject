
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

    public class TourOp
    {

        #region Proprietes

        [Key]
        [Display(Name = "TourOpID")]
        public int TourOpID { get; set; }

        [Required]
        [Display(Name = "CodeTourOp")]
        public int CodeTourOp { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Nationalite")]
        public string Nationalite { get; set; }

        [Required]
        [Display(Name = "Langue")]
        public string Langue { get; set; }

        #endregion

    }
}