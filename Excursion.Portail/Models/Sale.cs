using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Excursion.Portail.Models
{
    public class Sale
    {

        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Product { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal Amount { get; set; }
    }
}