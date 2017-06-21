using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Excursion.Portail.Models
{
    public class ReservationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ReservationID { get; set; }
    //System.Guid
        [Required(ErrorMessage = "UserID Required")]
        [DisplayName("UserID")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "TypeClient Required")]
        [DisplayName("TypeClient")]
        [StringLength(50, ErrorMessage = "Email must be less than or equal to 50 characters")]
        public string TypeClient { get; set; }

        [Required(ErrorMessage = "PointDepart Required")]
        [DisplayName("PointDepart")]
        [StringLength(50, ErrorMessage = "Phone must be less than or equal to 50 characters")]
        public string PointDepart { get; set; }

        [Required(ErrorMessage = "DateReserv Required")]
        [DisplayName("DateReserv")]
        public DateTime DateReserv { get; set; }

        [Required(ErrorMessage = "NumTicket")]
        [DisplayName("NumTicket")]
        public int NumTicket { get; set; }

        [Required(ErrorMessage = "NumChamb")]
        [DisplayName("NumChamb")]
        public int NumChamb { get; set; }
        
        [Required(ErrorMessage = "SortieID Required")]
        [DisplayName("SortieID")]
        public string SortieID { get; set; }
        
        [Required(ErrorMessage = "HotelID Required")]
        [DisplayName("HotelID")]
        public string HotelID { get; set; }

        
        [Required(ErrorMessage = "LangueID Required")]
        [DisplayName("LangueID")]
        public string LangueID { get; set; }
        
        [DisplayName("Observation")]
        public string Observation { get; set; }
        
        [Required(ErrorMessage = "Etat Required")]
        [DisplayName("Etat")]
        public string Etat { get; set; }
        
        [DisplayName("DateAnnulation")]
        public DateTime? DateAnnulation { get; set; }
        
        [DisplayName("DateModification")]
        public DateTime? DateModification { get; set; }
        
        [DisplayName("NbreAdultes")]
        public int? NbreAdultes { get; set; }
        
        [DisplayName("NbreEnfants")]
        public int? NbreEnfants { get; set; }
        
        [DisplayName("NbreBebes")]
        public int? NbreBebes { get; set; }
        
        [DisplayName("Paye")]
        public bool Paye { get; set; }
        
        [DisplayName("DatePayement")]
        public DateTime? DatePayement { get; set; }
        
    
    }
}