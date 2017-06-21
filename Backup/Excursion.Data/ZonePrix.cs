
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

    public class ZonePrix
    {
     
        #region Proprietes

        [Key]
        [Display(Name = "ZonePrixID")]
        public int PrixZoneID { get; set; }

        public int PrixID { get; set; }
        [ForeignKey("PrixID")]
        public virtual Prix Prix { get; set; }

        public int ZoneID { get; set; }
        [ForeignKey("ZoneID")]
        public virtual Zone Zone { get; set; }

        #endregion
    }
}