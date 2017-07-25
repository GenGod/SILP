using MongoDB.Driver;

namespace SILP.Model
{
    /// <summary>
    /// Class implements factory contract for MongoDB generic repositories
    /// </summary>
    public class MongoRepositoryFactory : IRepositoryFactory
    {
        private string databaseName;
        private string connectionString;

        /// <summary>
        /// MongoDB repository factory
        /// </summary>
        /// <param name="connectionString">Server connection settings</param>
        /// <param name="databaseName">Required database name</param>
        public MongoRepositoryFactory(string connectionString, string databaseName)
        {
            this.databaseName = databaseName;

            // if db connection string is not specified, use MongoDB default settins
            this.connectionString = connectionString ?? "mongodb://localhost:27017";
        }

        /// <summary>
        /// Returns a repository implementation for specified type
        /// </summary>
        /// <typeparam name="TEntity">Any Data Model type</typeparam>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            // find required db on server
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(this.databaseName);

            return new MongoRepository<TEntity>(db);
        }
    }
}
