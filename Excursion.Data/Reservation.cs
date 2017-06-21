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

    public class Reservation
    {

        #region Proprietes

        [Key]
        [Display(Name = "ReservationID")]
        public int ReservationID { get; set; }

        //public int ExcursionID { get; set; }
        //[ForeignKey("ExcursionID")]
        //public virtual Excursion Excursion { get; set; }

        public int SortieID { get; set; }
        [ForeignKey("SortieID")]
        public virtual SortieParSemaine SortieParSemaine { get; set; }

        //[Display(Name = "DateExcursion")]
        //public DateTime DateExcursion { get; set; }

        [Required]
        [Display(Name = "DateReserv")]
        public DateTime DateReserv { get; set; }

        //[StringLength(450)]
        [Display(Name = "NumChamb")]
        public string NumChamb { get; set; }

        public int? HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }

        [Required]
        [Display(Name = "TypeClient")]
        public string TypeClient { get; set; }

        
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User  { get; set; }



        //public int? CIndirectID { get; set; }
        //[ForeignKey("CIndirectID")]
        //public virtual CIndirect CIndirect { get; set; }


        //public int? ClientID { get; set; }
        //[ForeignKey("ClientID")]
        //public virtual Client Client { get; set; }

        [Required]
        [Display(Name = "PointDepart")]
        public string PointDepart { get; set; }


        [Display(Name = "NumLigneAs400")]
        public int? NumLigneAs400 { get; set; }

        public int? LangueID { get; set; }
        [ForeignKey("LangueID")]
        public virtual Langue Langue { get; set; }

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

        [Required]
        [Display(Name = "NumTicket")]
        public int NumTicket { get; set; }

        
        [Display(Name = "Reduction")]
        public Single? Reduction { get; set; }


        [Display(Name = "NbreAdultes")]
        public int? NbreAdultes { get; set; }


        [Display(Name = "NbreEnfants")]
        public int? NbreEnfants { get; set; }

        [Display(Name = "NbreBebes")]
        public int? NbreBebes  { get; set; }

        
        [Display(Name = "Paye")]
        public bool? Paye { get; set; }

        
        [Display(Name = "DatePayement")]
        public DateTime? DatePayement { get; set; }


        #endregion
    }
}