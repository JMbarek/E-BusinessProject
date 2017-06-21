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

    public class User
    {

        #region Proprietes
        [Key]
        [Display(Name = "UserID")]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "TypeUser")]
        public string TypeUser { get; set; }

        [Display(Name = "TypeCIndirect")]
        public string TypeCIndirect { get; set; }

        [Display(Name = "CodeCIndirect")]
        public int? CodeCIndirect { get; set; }

        public int? TourOpID { get; set; }
        [ForeignKey("TourOpID")]
        public virtual TourOp TourOp { get; set; }

        public int? CentreID { get; set; }
        [ForeignKey("CentreID")]
        public virtual Centre Centre { get; set; }

        [Display(Name = "RoleResp")]
        public string RoleResp { get; set; }

        [Display(Name = "nom")]
        public string Nom { get; set; }

        [Display(Name = "prenom")]
        public string Prenom { get; set; }

        [Display(Name = "Nationalite")]
        public string Nationalite { get; set; }

        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

        [Display(Name = "CodePostal")]
        public int? CodePostal { get; set; }

        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Display(Name = "Pays")]
        public string Pays { get; set; }

        [Display(Name = "TypeDoc")]
        public string TypeDoc { get; set; }

        [Display(Name = "NumCin")]
        public int? NumCin { get; set; }

        [Display(Name = "NumPassport")]
        public int? NumPassport { get; set; }

        [Display(Name = "TelephoneFix")]
        public int? TelephoneFix { get; set; }

        [Display(Name = "TelephoneMobile")]
        public int? TelephoneMobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "DateNaissance")]
        public DateTime? DateNaissance { get; set; }

        [Display(Name = "Fonction")]
        public string Fonction { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "PayementRight")]
        public bool? PayementRight { get; set; }

        [Display(Name = "Actif")]
        public bool? Actif { get; set; }


        #endregion
    }
}