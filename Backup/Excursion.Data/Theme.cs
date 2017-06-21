
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

    public class Theme
    {

        #region Proprietes

        [Key]
        [Display(Name = "ThemeID")]
        public int ThemeID { get; set; }

        [Required]
        [Display(Name = "Nom_fr")]
        public string Nom_fr { get; set; }

        [Required]
        [Display(Name = "Nom_de")]
        public string Nom_de { get; set; }

        [Required]
        [Display(Name = "Nom_en")]
        public string Nom_en { get; set; }

        [Required]
        [Display(Name = "Nom_it")]
        public string Nom_it { get; set; }

        [Required]
        [Display(Name = "Description_fr")]
        public string Description_fr { get; set; }

        [Required]
        [Display(Name = "Description_en")]
        public string Description_en { get; set; }

        [Required]
        [Display(Name = "Description_de")]
        public string Description_de { get; set; }

        [Required]
        [Display(Name = "Description_it")]
        public string Description_it { get; set; }

        [Required]
        [Display(Name = "CodeTheme")]
        public int CodeTheme { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        #endregion

    }
}