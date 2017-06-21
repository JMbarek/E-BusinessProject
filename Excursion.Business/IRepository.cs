//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="Excursion">
//     Copyright (c) Excursion. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Excursion.Business
{
    #region using directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;


    #endregion

    /// <summary>
    /// Generic Repository Pattern Interface
    /// </summary>
    /// <typeparam name="TEntity">A POCO Entity Type</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Methods

        /// <summary>
        /// Create a new Entity Object
        /// </summary>
        /// <returns>Entity object of type <typeparamref name="TEntity"/></returns>
        TEntity Create();

        /// <summary>
        /// Gets all entities of type TEntity.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Finds the entity by its key.
        /// </summary>
        /// <param name="id">the id of the entity to find</param>
        TEntity GetById(int id);

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        //// IEnumerable<TEntity> FindMany(ISpecification<TEntity> criteria);

        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        //// TEntity FindOne(ISpecification<TEntity> criteria);

        /// <summary>
        /// Delete the Entity object from the repository
        /// </summary>
        /// <param name="entity">The Entity object to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete the Entity object from the repository
        /// </summary>
        /// <param name="id">The id of the Entity object to delete</param>
        void Delete(int id);

        /// <summary>
        /// Add the Entity Object to the Repository
        /// </summary>
        /// <param name="entity">The Entity object to add</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update changes made to the Entity object in the repository
        /// </summary>
        /// <param name="entity">The Entity object to update</param>
        void Update(TEntity entity);

        #endregion
    }
}
