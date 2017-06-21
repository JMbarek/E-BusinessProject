using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Excursion.Portail.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class Register2Model
    {

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

        [Display(Name = "TourOpID")]
        public int? TourOpID { get; set; }

        [Display(Name = "CentreID")]
        public int? CentreID { get; set; }
        //[ForeignKey("RegionID")]
        //public virtual Region Region { get; set; }

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

        //[Required]
        //[Display(Name = "ISerie name")]
        //public string ISerieName { get; set; }

        //[Required]
        //[Display(Name = "User name")]
        //public string UserName { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }


    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
