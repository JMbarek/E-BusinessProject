using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Excursion.Portail.Models
{
    public class jsonObject
    {
        [JsonProperty("jsonString")]
        public List<CartParam> jsonString { get; set; }
    }
    
    public class CartParam 
    {

        [JsonProperty("ExcursionnID")]
        public string ExcursionnID { get; set; }

        [JsonProperty("NbreAdultes")]
        public int NbreAdultes { get; set; }

        [JsonProperty("NbreEnfants")]
        public int NbreEnfants { get; set; }

        [JsonProperty("NbreBebes")]
        public int NbreBebes { get; set; }

    }
}