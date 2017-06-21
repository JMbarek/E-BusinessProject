namespace Excursion.Data
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Script.Serialization;
    using System.Web;
    #endregion

    public class Excursionn
    {

        #region Proprietes
        [Key]
        //[ScriptIgnore]
        [Display(Name = "excursionnId")]
        public string excursionnId { get; set; }

        [Display(Name = "organizerId")]
        public int organizerId { get; set; }

        [Display(Name = "title")]
        public string title { get; set; }

        [Display(Name = "description")]
        public string description { get; set; }

        [Display(Name = "theme")]
        public string theme { get; set; }

        [Display(Name = "state")]
        public bool state { get; set; }

        [Display(Name = "adults")]
        public int adults { get; set; }

        [Display(Name = "kinder")]
        public int kinder { get; set; }

        [Display(Name = "babies")]
        public int babies { get; set; }

        [Display(Name = "priceAdult")]
        public float priceAdult { get; set; }

        [Display(Name = "priceKind")]
        public float priceKind { get; set; }

        [Display(Name = "priceBaby")]
        public float priceBaby { get; set; }

        [Display(Name = "departureDay")]
        public string departureDay { get; set; }

        [Display(Name = "departureTime")]
        public string departureTime { get; set; }

        [Display(Name = "departurePoint")]
        public string departurePoint { get; set; }

        [Display(Name = "departureCountry")]
        public string departureCountry { get; set; }

        [Display(Name = "destination")]
        public string destination { get; set; }

        [Display(Name = "length")]
        public string length { get; set; }

        [Display(Name = "createdOn")]
        public string createdOn { get; set; }

        [Display(Name = "updatedAt")]
        public string updatedAt { get; set; }

        #endregion
    }

    public class FileUploadViewModel
    {
        [Key]
        public int FileUploadID { get; set; }
        public string FileType { get; set; }
        public string FileUploadLocation { get; set; }
    }
}