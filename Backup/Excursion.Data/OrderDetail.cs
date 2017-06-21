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

    public class OrderDetail
    {
        [Key]
        [Display(Name = "OrderDetailId")]
        public int OrderDetailId { get; set; }

        [Display(Name = "QuantityAd")]
        public int QuantityAd { get; set; }

        [Display(Name = "UnitPriceAd")]
        public decimal UnitPriceAd { get; set; }

        [Display(Name = "QuantityEn")]
        public int QuantityEn { get; set; }

        [Display(Name = "UnitPriceEn")]
        public decimal UnitPriceEn { get; set; }

        [Display(Name = "QuantityBb")]
        public int QuantityBb { get; set; }

        [Display(Name = "UnitPriceBb")]
        public decimal UnitPriceBb { get; set; }

        public int SortieID { get; set; }
        [ForeignKey("SortieID")]
        public virtual SortieParSemaine SortieParSemaine { get; set; }
        
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}