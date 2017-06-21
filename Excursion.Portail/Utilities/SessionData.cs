
//-----------------------------------------------------------------------
// <copyright file="SessionData.cs" company="Jawhar">
//     Copyright (c) Jawhar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace Excursion.Portail.Utilities
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Excursion.Data;
    #endregion

    /// <summary>
    /// Une classe pour gérer les membres de la session utilisateur
    /// </summary>
    public class SessionData
    {
        /// <summary>
        /// Utilisateur courant
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["CurrentUser"] != null)
                {
                    return (User)HttpContext.Current.Session["CurrentUser"];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    HttpContext.Current.Session["CurrentUser"] = value;
                }
            }
        }

    }
}