
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

    public class Zone
    {

        #region Proprietes

        [Key]
        [Display(Name = "ZoneID")]
        public int ZoneID { get; set; }

        [Required]
        [Display(Name = "CodeZone")]
        public int CodeZone { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        public int CentreID { get; set; }
        [ForeignKey("CentreID")]
        public virtual Centre Centre { get; set; }

        #endregion

    }
}