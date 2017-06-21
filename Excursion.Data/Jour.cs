
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

    public class Jour
    {
        #region Proprietes
        [Key]
        [Display(Name = "JourID")]
        public int JourID { get; set; }

        [Required]
        [Display(Name = "NumeroJ")]
        public int NumeroJ { get; set; }

       
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Etat")]
        public string Etat { get; set; }

        #endregion

    }
}
