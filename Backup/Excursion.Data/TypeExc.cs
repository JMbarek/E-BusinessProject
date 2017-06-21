
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

    public class TypeExc
    {
        #region Proprietes
        [Key]
        [Display(Name = "TypeExcID")]
        public int TypeExcID { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        #endregion
    }
}
