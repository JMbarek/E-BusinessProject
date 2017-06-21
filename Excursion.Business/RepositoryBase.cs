//-----------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="NetQuarks">
//     Copyright (c) TTS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Excursion.Business
{
    #region using directives

    //using Excursion.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Excursion.Data;

    #endregion

    /// <summary>
    /// Repository abstract base class which must be implemented by the concrete repository.
    /// It provides the abstraction that the client component can program against directly and
    /// in the mean time serves as an interface that its child type must implement.
    /// </summary>
    /// <typeparam name="TEntity">The type of the Entity Object</typeparam>    
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, IQueryable<TEntity>, IDisposable
        where TEntity : class
    {
        /// <summary>
        /// Gets the repository query.
        /// </summary>
        /// <value>The repository query.</value>
        protected virtual internal IQueryable<TEntity> RepositoryQuery { get; set; }

        /// <summary>
        /// Gets the database context
        /// </summary>
        public virtual ExcursionContext context { get; set; }


        #region IEnumerable<TEntity> Members

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return RepositoryQuery.GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType
        {
            get
            {
                return RepositoryQuery.ElementType;
            }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.</returns>
        public Expression Expression
        {
            get
            {
                return RepositoryQuery.Expression;
            }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.</returns>
        public IQueryProvider Provider
        {
            get
            {
                return RepositoryQuery.Provider;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.context != null)
            {
                context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///Finalizesan instance of the repositoryBase class
        ///Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="RepositoryBase&lt;TEntity&gt;"/> is reclaimed by garbage collection.
        /// </summary>
        ~RepositoryBase()
        {
            // Simply call Dispose(false).
            Dispose();
        }
        #endregion

        /// <summary>
        /// Création d'une nouvelle instance de type TEntity.
        /// </summary>
        public abstract TEntity Create();

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type Type.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Avoir une IQueryable sequence des entités de type Type.
        /// </summary>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetAllAsQueryable();

        /// <summary>
        /// Avoir l'entité avec id= @id .
        /// </summary>
        /// <param name="id">l'id de l'entité voulu.</param>
        /// <returns>l'entité avec id= @id .</returns>
        public abstract TEntity GetById(int id);

        // public abstract IEnumerable<TEntity> FindMany(ISpecification<TEntity> criteria);

        /// <summary>
        /// Avoir une IEnumerable sequence des entités de type Type filtered on the @where predicate.
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public abstract IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> expression);

        ////public abstract TEntity FindOne(ISpecification<TEntity> criteria);

        /// <summary>
        /// Avoir une seule entité depuis une séquence des entités de type Type en filtrant avec le prédicat @where .
        /// </summary>
        /// <param name="where">Le prédicat where.</param>
        /// <returns></returns>
        public abstract TEntity FindOne(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Suppression de l'entité specifiée. 
        /// </summary>
        /// <param name="entity">l'entité à supprimer.</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        ///Suppression de l'entité avec id = @id.
        /// </summary>
        /// <param name="id">id de l'entité à supprimer.</param>
        public abstract void Delete(int id);

        /// <summary>
        /// Ajout de l'entité specifiée au respository the type TEntity.
        /// </summary>
        /// <param name="entity">l'entité à ajouter.</param>
        public abstract void Add(TEntity entity);

        /// <summary>
        /// Modification de l'entité specifiée
        /// </summary>
        /// <param name="entity">L'entité à modifier.</param>
        public abstract void Update(TEntity entity);

        /// <summary>
        /// Sauvegarder dans le context les changements
        /// </summary>
        public abstract void Save();


    }
}
