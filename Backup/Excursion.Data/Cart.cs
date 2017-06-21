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

    public class Cart
    {
        #region Proprietes
        [Key]
        [Display(Name = "RecordId")]
        public int RecordId { get; set; }

        [Display(Name = "CartId")]
        public string CartId { get; set; }

        [Display(Name = "CountAdulte")]
        public int CountAdulte { get; set; }

        [Display(Name = "CountEnfant")]
        public int CountEnfant { get; set; }

        [Display(Name = "CountBebe")]
        public int CountBebe { get; set; }

        [Display(Name = "DateCreated")]
        public System.DateTime DateCreated { get; set; }

        public int SortieID { get; set; }
        [ForeignKey("SortieID")]
        public virtual SortieParSemaine SortieParSemaine { get; set; }

        #endregion
    }
}