using System.Linq;

namespace SILP.Model
{
    /// <summary>
    /// Interfaces declares generic repository contract
    /// </summary>
    /// <typeparam name="TEntity">Any Data Model type</typeparam>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Returns single item by its identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns></returns>
        TEntity Get(string id);

        /// <summary>
        /// Performs Insert or Update of an item
        /// </summary>
        /// <param name="entity">Entity to save</param>
        void Save(TEntity entity);

        /// <summary>
        /// Deletes a single item by its identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        void Delete(string id);

        /// <summary>
        /// Deletes a single item
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Returns queryable context to fetch data
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();
    }
}
