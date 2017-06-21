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

    public class Order
    {
        [Key]
        [Display(Name = "OrderId")]
        public int OrderId { get; set; }

        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        //public string Username { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string PostalCode { get; set; }
        //public string Country { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "OrderDate")]
        public System.DateTime OrderDate { get; set; }

        [Display(Name = "OrderDetails")]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}