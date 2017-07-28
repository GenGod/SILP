using MongoDB.Driver;
using System;
using System.Linq;

namespace SILP.Model
{
    /// <summary>
    /// Class implements generic repository contract for MongoDB
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private IMongoDatabase database;
        private string collectionName;

        public MongoRepository(IMongoDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));

            this.database = database;
            collectionName = typeof(TEntity).Name;
        }

        /// <summary>
        /// Returns database collection of specified type
        /// </summary>
        /// <returns></returns>
        private IMongoCollection<TEntity> GetCollection()
        {
            return this.database.GetCollection<TEntity>(collectionName);
        }

        /// <summary>
        /// Returns single item by its identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        public TEntity Get(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            return this.GetCollection().Find(e => e.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Performs Insert or Update of an item
        /// </summary>
        /// <param name="entity">Entity to save</param>
        public void Save(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var collection = this.GetCollection();
            if (entity.Id == null)
                collection.InsertOne(entity);
            else
                collection.ReplaceOne(item => item.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = true });
        }

        /// <summary>
        /// Deletes a single item by its identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        public void Delete(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            this.GetCollection().FindOneAndDelete(e => e.Id.Equals(id));
        }

        /// <summary>
        /// Deletes a single item
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.GetCollection().DeleteOne(e => e.Id.Equals(entity.Id));
        }

        /// <summary>
        /// Returns queryable context to fetch data
        /// </summary>
        public IQueryable<TEntity> Query()
        {
            return this.GetCollection().AsQueryable();
        }
    }
}
