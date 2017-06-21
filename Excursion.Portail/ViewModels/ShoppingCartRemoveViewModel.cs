using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Excursion.Portail.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCountAdulte { get; set; }
        public int ItemCountEnfant { get; set; }
        public int ItemCountBebe { get; set; }
        public int DeleteId { get; set; }
    }
}