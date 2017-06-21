using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excursion.Portail.Models
{
    public class ReservationModel2
    {

            [Display(Name = "ReservationID")]
            public int ReservationID { get; set; }

            public string Sortie { get; set; }
            public int SortieID { get; set; }
            //[ForeignKey("SortieID")]
            //public virtual SortieParSemaine SortieParSemaine { get; set; }

            [Required]
            [Display(Name = "DateReserv")]
            public DateTime DateReserv { get; set; }

            [Display(Name = "NumChamb")]
            //[Required(ErrorMessage = "NumChamb Required")]
            //[StringLength(5, ErrorMessage = "NumChamb must be less than or equal to 5 characters")]
            public string NumChamb { get; set; }

            [Required(ErrorMessage = "Hotel Required")]
            [DisplayName("Hotel")]
            //[StringLength(50, ErrorMessage = "Hotel must be less than or equal to 50 characters")]
            public string Hotel { get; set; }
            //public int? HotelID { get; set; }
            //[ForeignKey("HotelID")]
            //public virtual Hotel Hotel { get; set; }

            [Required]
            [Display(Name = "TypeClient")]
            public string TypeClient { get; set; }


            public int UserID { get; set; }
            //[ForeignKey("UserID")]
            //public virtual User User  { get; set; }

            [Required]
            [Display(Name = "PointDepart")]
            public string PointDepart { get; set; }


            [Display(Name = "NumLigneAs400")]
            public int? NumLigneAs400 { get; set; }

            public string Langue { get; set; }
            //public int? LangueID { get; set; }
            //[ForeignKey("LangueID")]
            //public virtual Langue Langue { get; set; }

            [StringLength(15)]
            [Display(Name = "Observation")]
            public string Observation { get; set; }

            [Required]
            [StringLength(1)]
            [Display(Name = "Etat")]
            public string Etat { get; set; }

            [Display(Name = "DateAnnulation")]
            public DateTime? DateAnnulation { get; set; }

            [Display(Name = "DateModification")]
            public DateTime? DateModification { get; set; }


            [Required(ErrorMessage = "NumTicket Required")]
            //[MaxLength(15, ErrorMessage = "NumTicket max length is 15 numbers")]
            //[MinLength(1, ErrorMessage = "NumTicket max length is 1 numbers")]
            [Display(Name = "NumTicket")]
            public int NumTicket { get; set; }

            [Display(Name = "Reduction")]
            public Single? Reduction { get; set; }

            [Display(Name = "NbreAdultes")]
            public int? NbreAdultes { get; set; }

            [Display(Name = "NbreEnfants")]
            public int? NbreEnfants { get; set; }

            [Display(Name = "NbreBebes")]
            public int? NbreBebes { get; set; }

            [Display(Name = "Paye")]
            public bool Paye { get; set; }

            [Display(Name = "DatePayement")]
            public DateTime? DatePayement { get; set; }

            [DisplayName("AddRow")]
            public int? AddRow { get; set; }

            [DisplayName("DeleteRow")]
            public int? DeleteRow { get; set; }

            [DisplayName("UpdateRow")]
            public int? UpdateRow { get; set; }

            [DisplayName("PayeRow")]
            public int? PayeRow { get; set; }

            [DisplayName("Guide")]
            public string Guide { get; set; }

            [DisplayName("Fournisseur")]
            public string Fournisseur { get; set; }

            [DisplayName("ClientD")]
            public string ClientD { get; set; }

        


    }
}