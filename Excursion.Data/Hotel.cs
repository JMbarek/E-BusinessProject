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

    public class Hotel
    {

        #region Proprietes

        [Key]
        [Display(Name = "HotelID")]
        public int HotelID { get; set; }

        [Required]
        [Display(Name = "CodeHotel")]
        public int CodeHotel { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "NbrEtoiles")]
        public string NbrEtoiles { get; set; }

        public int? RegionID { get; set; }
        [ForeignKey("RegionID")]
        public virtual Region Region { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }


        #endregion

    }
}
