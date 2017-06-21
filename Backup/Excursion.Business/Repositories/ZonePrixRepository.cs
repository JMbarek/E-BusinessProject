//-----------------------------------------------------------------------
// <copyright file="ZonePrixRepository.cs" company="Jawhar">
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
    /// this is a ZonePrixRepository class
    /// </summary>
    public class ZonePrixRepository : RepositoryBase<Excursion.Data.ZonePrix>
    {
        #region Fields

        ///<summary>
        ///le dbSet des entités de type ZonePrix
        ///</summary>
        public DbSet<Excursion.Data.ZonePrix> BoxSet;

        #endregion Fields

        #region Ctors

        ///<summary>
        ///Le constructeur du ZonePrixRepository
        ///</summary>
        ///<param name="contextParam">Le context de la base de donnée.</param>
        public ZonePrixRepository(ExcursionContext contextParam)
        {
            this.context = contextParam;
            this.BoxSet = context.ZonePrixSet;
        }

        #endregion Ctors

        #region Methods

        /// <summary>
        /// Création d'une nouvelle instance de type ZonePrix.
        /// </summary>
        public override Excursion.Data.ZonePrix Create()
        {
            return new Excursion.Data.ZonePrix();
        }

        /// <summary>
        /// Ajout de l'entité specifiée au respository de type ZonePrix.
        /// </summary>
        /// <param name="entity">l'entité à ajouter.</param>
        public override void Add(Excursion.Data.ZonePrix entity)
        {
            BoxSet.Add(entity);
        }

        /// <summary>
        /// Modification de l'entité specifiée
        /// </summary>
        /// <param name="entity">L'entité à modifier.</param>
        public override void Update(Excursion.Data.ZonePrix entity)
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
            Excursion.Data.ZonePrix entityToDelete = BoxSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Suppression de l'entité specifiée. 
        /// </summary>
        /// <param name="entity">l'entité à supprimer.</param>
        public override void Delete(Excursion.Data.ZonePrix entityToDelete)
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
        public override Excursion.Data.ZonePrix GetById(int id)
        {
            return BoxSet.Find(id);
        }


        /// <summary>
        /// Avoir une IQueryable sequence des entités de type ZonePrix.
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Excursion.Data.ZonePrix> GetAllAsQueryable()
        {
            return BoxSet;
        }

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type ZonePrix.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Excursion.Data.ZonePrix> GetAll()
        {
            return BoxSet.ToList();
        }

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type ZonePrix filtered on the @where predicate.
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public override IEnumerable<Excursion.Data.ZonePrix> FindMany(Expression<Func<Excursion.Data.ZonePrix, bool>> where)
        {
            return BoxSet.Where(where);
        }

        /// <summary>
        /// Avoir une seule entité depuis une séquence des entités de type ZonePrix en filtrant avec le prédicat @where .
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public override Excursion.Data.ZonePrix FindOne(Expression<Func<Excursion.Data.ZonePrix, bool>> where)
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


