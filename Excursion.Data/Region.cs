
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

    public class Region
    {
        #region Proprietes

        [Key]
        [Display(Name = "RegionID")]
        public int RegionID { get; set; }

        [Required]
        [Display(Name = "CodeRegion")]
        public int CodeRegion { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        public int ZoneID { get; set; }
        [ForeignKey("ZoneID")]
        public virtual Zone Zone { get; set; }

        #endregion
    }
}