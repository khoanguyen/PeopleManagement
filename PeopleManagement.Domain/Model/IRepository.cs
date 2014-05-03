using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.Domain.Model
{
    /// <summary>
    /// Interface for Entity Repository implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// PeopleDB context
        /// </summary>
        PeopleContext Context { get; }
        
        /// <summary>
        /// Returns a list of given entity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> List();

        /// <summary>
        /// Gets an entity based on its keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        TEntity Get(params object[] keys);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates a new entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}
