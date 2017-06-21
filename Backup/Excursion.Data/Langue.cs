
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

   public class Langue
    {

        #region Proprietes
        [Key]
        [Display(Name = "LangueID")]
        public int LangueID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "CodeLangue")]
        public string CodeLangue { get; set; }

        
        [Display(Name = "NomLangue")]
        public string NomLangue { get; set; }

        #endregion
    }
}
