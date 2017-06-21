//-----------------------------------------------------------------------
// <copyright file="ReservationRepository.cs" company="Jawhar">
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
    /// this is a ReservationRepository class
    /// </summary>
    public class ReservationRepository : RepositoryBase<Excursion.Data.Reservation>
    {
        #region Fields

        ///<summary>
        ///le dbSet des entités de type Reservation
        ///</summary>
        public DbSet<Excursion.Data.Reservation> BoxSet;

        #endregion Fields

        #region Ctors

        ///<summary>
        ///Le constructeur du ReservationRepository
        ///</summary>
        ///<param name="contextParam">Le context de la base de donnée.</param>
        public ReservationRepository(ExcursionContext contextParam)
        {
            this.context = contextParam;
            this.BoxSet = context.ReservationSet;
        }

        #endregion Ctors

        #region Methods

        /// <summary>
        /// Création d'une nouvelle instance de type Reservation.
        /// </summary>
        public override Excursion.Data.Reservation Create()
        {
            return new Excursion.Data.Reservation();
        }

        /// <summary>
        /// Ajout de l'entité specifiée au respository de type Reservation.
        /// </summary>
        /// <param name="entity">l'entité à ajouter.</param>
        public override void Add(Excursion.Data.Reservation entity)
        {
            BoxSet.Add(entity);
        }

        /// <summary>
        /// Modification de l'entité specifiée
        /// </summary>
        /// <param name="entity">L'entité à modifier.</param>
        public override void Update(Excursion.Data.Reservation entity)
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
            Excursion.Data.Reservation entityToDelete = BoxSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Suppression de l'entité specifiée. 
        /// </summary>
        /// <param name="entity">l'entité à supprimer.</param>
        public override void Delete(Excursion.Data.Reservation entityToDelete)
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
        public override Excursion.Data.Reservation GetById(int id)
        {
            return BoxSet.Find(id);
        }


        /// <summary>
        /// Avoir une IQueryable sequence des entités de type Reservation.
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Excursion.Data.Reservation> GetAllAsQueryable()
        {
            return BoxSet;
        }

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type Reservation.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Excursion.Data.Reservation> GetAll()
        {
            return BoxSet.ToList();
        }

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type Reservation filtered on the @where predicate.
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public override IEnumerable<Excursion.Data.Reservation> FindMany(Expression<Func<Excursion.Data.Reservation, bool>> where)
        {
            return BoxSet.Where(where);
        }

        /// <summary>
        /// Avoir une seule entité depuis une séquence des entités de type Reservation en filtrant avec le prédicat @where .
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public override Excursion.Data.Reservation FindOne(Expression<Func<Excursion.Data.Reservation, bool>> where)
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


