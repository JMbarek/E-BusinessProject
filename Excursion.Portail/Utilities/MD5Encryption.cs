//-----------------------------------------------------------------------
// <copyright file="MD5Encryption.cs" company="Jawhar">
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
    using System.Security.Cryptography;
    using System.Text;
    #endregion

    /// <summary>
    /// Classe de la gestion de cryptage en utilisant MD5
    /// </summary>
    public class MD5Encryption
    {
        #region Fields
        /// <summary>
        /// Valeur aléatoire
        /// </summary>
        private static Random random = new Random((int)DateTime.Now.Ticks);
        #endregion

        #region Methods
        /// <summary>
        /// Génère une chanine aléatoire
        /// </summary>
        /// <param name="size">Longueur de la chaine</param>
        /// <returns></returns>
        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Méthode permet de générer une chaine de caractère cryptée
        /// </summary>
        /// <param name="chaine"></param>
        /// <returns></returns>
        public static string EncrypterMD5(string chaine)
        {
            if (!string.IsNullOrEmpty(chaine))
            {
                UnicodeEncoding monConvertisseur = new UnicodeEncoding();
                MD5CryptoServiceProvider monCrypteur = new MD5CryptoServiceProvider();
                byte[] maChaineToHash = monConvertisseur.GetBytes(chaine);
                byte[] hashValue = monCrypteur.ComputeHash(maChaineToHash);
                String passwdCrypte = String.Empty;
                for (int i = 0; i < hashValue.Length; i++)
                {
                    passwdCrypte += String.Format("{0:X2}", hashValue[i]);
                }
                return passwdCrypte;
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
