
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

    public class Excursion
    {

        #region Proprietes

        [Key]
        [Display(Name = "ExcursionID")]
        public int ExcursionID { get; set; }

        [Required]
        [Display(Name = "CodeExcursion")]
        public int CodeExcursion { get; set; }

        [Required]
        [Display(Name = "Nom_fr")]
        public string Nom_fr { get; set; }

        [Required]
        [Display(Name = "Nom_en")]
        public string Nom_en { get; set; }

        [Required]
        [Display(Name = "Nom_de")]
        public string Nom_de { get; set; }

        [Required]
        [Display(Name = "Nom_it")]
        public string Nom_it { get; set; }

        public int CentreID { get; set; }
        [ForeignKey("CentreID")]
        public virtual Centre Centre { get; set; }

        [Required]
        [Display(Name = "Duree")]
        public string Duree { get; set; }

        [Required]
        [Display(Name = "DureeHeure")]
        public string DureeHeure { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        public int PeriodeID { get; set; }
        [ForeignKey("PeriodeID")]
        public virtual Periode Periode { get; set; }

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
        [Display(Name = "Etat")]
        public string Etat { get; set; }

        public int PrixID { get; set; }
        [ForeignKey("PrixID")]
        public virtual Prix Prix { get; set; }

        #endregion
    }
}