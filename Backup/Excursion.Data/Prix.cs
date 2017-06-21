
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

    public class Prix
    {
        #region Proprietes
        [Key]
        [Display(Name = "PrixID")]
        public int PrixID { get; set; }

        
        [Display(Name = "DateDebut")]
        public DateTime? DateDebut { get; set; }

        
        [Display(Name = "DateFin")]
        public DateTime? DateFin { get; set; }

        [Required]
        [Display(Name = "PrixAdulte")]
        public Single PrixAdulte { get; set; }

        [Required]
        [Display(Name = "PrixEnfant")]
        public Single PrixEnfant { get; set; }

        [Required]
        [Display(Name = "PrixBebe")]
        public Single PrixBebe { get; set; }

        [Display(Name = "Monnaie")]
        public string Monnaie { get; set; }

        #endregion
    }
}
