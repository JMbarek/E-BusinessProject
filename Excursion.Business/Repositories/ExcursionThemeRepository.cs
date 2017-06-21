//-----------------------------------------------------------------------
// <copyright file="ExcursionThemeRepository.cs" company="Jawhar">
//      Copyright ( c ) Jawhar. All rights reserved.
// </copyright>
// <summary>This is the Context class.</summary>
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Excursion.Data;
using Excursion.Business;


namespace Excursion.Business.Repositories
{
    #region directives


    #endregion
    /// <summary>
    /// this is a ExcursionThemeRepository class
    /// </summary>
    public class ExcursionThemeRepository : RepositoryBase<Excursion.Data.ExcursionTheme>
    {
        #region Fields

        ///<summary>
        ///le dbSet des entités de type ExcursionTheme
        ///</summary>
        public DbSet<Excursion.Data.ExcursionTheme> BoxSet;

        #endregion Fields

        #region Ctors

        ///<summary>
        ///Le constructeur du ExcursionThemeRepository
        ///</summary>
        ///<param name="contextParam">Le context de la base de donnée.</param>
        public ExcursionThemeRepository(ExcursionContext contextParam)
        {
            this.context = contextParam;
            this.BoxSet = context.ExcursionThemeSet;
        }

        #endregion Ctors

        #region Methods

        /// <summary>
        /// Création d'une nouvelle instance de type ExcursionTheme.
        /// </summary>
        public override Excursion.Data.ExcursionTheme Create()
        {
            return new Excursion.Data.ExcursionTheme();
        }

        /// <summary>
        /// Ajout de l'entité specifiée au respository de type ExcursionTheme.
        /// </summary>
        /// <param name="entity">l'entité à ajouter.</param>
        public override void Add(Excursion.Data.ExcursionTheme entity)
        {
            BoxSet.Add(entity);
        }

        /// <summary>
        /// Modification de l'entité specifiée
        /// </summary>
        /// <param name="entity">L'entité à modifier.</param>
        public override void Update(Excursion.Data.ExcursionTheme entity)
        {
            BoxSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        ///Suppression de l'entité avec id = @id.
        /// </summary>
        /// <param name="id">id de l'entité à supprimer.</param>
        public override void Delete(int id)
        {
            Excursion.Data.ExcursionTheme entityToDelete = BoxSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Suppression de l'entité specifiée. 
        /// </summary>
        /// <param name="entity">l'entité à supprimer.</param>
        public override void Delete(Excursion.Data.ExcursionTheme entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                BoxSet.Attach(entityToDelete);
            }
            BoxSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Avoir l'entité avec id= @id .
        /// </summary>
        /// <param name="id">l'id de l'entité voulu.</param>
        /// <returns></returns>
        public override Excursion.Data.ExcursionTheme GetById(int id)
        {
            return BoxSet.Find(id);
        }


        /// <summary>
        /// Avoir une IQueryable sequence des entités de type ExcursionTheme.
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Excursion.Data.ExcursionTheme> GetAllAsQueryable()
        {
            return BoxSet;
        }

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type ExcursionTheme.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Excursion.Data.ExcursionTheme> GetAll()
        {
            return BoxSet.ToList();
        }

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type ExcursionTheme filtered on the @where predicate.
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public override IEnumerable<Excursion.Data.ExcursionTheme> FindMany(Expression<Func<Excursion.Data.ExcursionTheme, bool>> where)
        {
            return BoxSet.Where(where);
        }

        /// <summary>
        /// Avoir une seule entité depuis une séquence des entités de type ExcursionTheme en filtrant avec le prédicat @where .
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public override Excursion.Data.ExcursionTheme FindOne(Expression<Func<Excursion.Data.ExcursionTheme, bool>> where)
        {
            return BoxSet.FirstOrDefault(where);
        }

        /// <summary>
        /// Sauvegarder dans le context les changements
        /// </summary>
        public override void Save()
        {
            context.SaveChanges();
        }
        #endregion Methods


    }
}


