
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

    public class SortieParSemaine
    {
        #region Proprietes
        [Key]
        [Display(Name = "SortieID")]
        public int SortieID { get; set; }

        public int JourID { get; set; }
        [ForeignKey("JourID")]
        public virtual Jour Jour { get; set; }

        [Required]
        [Display(Name = "HeureDepart")]
        public Single HeureDepart { get; set; }

        public int TypeExcID { get; set; }
        [ForeignKey("TypeExcID")]
        public virtual TypeExc TypeExc  { get; set; }

        public int ExcursionID { get; set; }
        [ForeignKey("ExcursionID")]
        public virtual Excursion Excursion { get; set; }

        #endregion
    }
}