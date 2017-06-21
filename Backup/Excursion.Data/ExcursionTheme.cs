
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

    public class ExcursionTheme
    {

        #region Proprietes

        [Key]
        [Display(Name = "ExcursionThemeID")]
        public int ExcursionThemeID { get; set; }
        
        public int ExcursionID { get; set; }
        [ForeignKey("ExcursionID")]
        public virtual Excursion Excursion { get; set; }

        public int ThemeID { get; set; }
        [ForeignKey("ThemeID")]
        public virtual Theme Theme { get; set; }

        #endregion

    }
}