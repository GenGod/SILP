using System;

namespace SILP.Model
{
    /// <summary>
    /// Declares factory contract for generic repositories
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Returns a repository implementation for specified type
        /// </summary>
        /// <typeparam name="TEntity">Any Data Model type</typeparam>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    }
}
