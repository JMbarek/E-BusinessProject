

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

    public class Centre
    {
        #region Proprietes

        [Key]
        [Display(Name = "CentreID")]
        public int CentreID { get; set; }
        
        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }
        
        [Required]
        [Display(Name = "CodeCentre")]
        public int CodeCentre { get; set; }

        [Required]
        [Display(Name = "AdresseIP")]
        public string AdresseIP { get; set; }

        #endregion


    }
}