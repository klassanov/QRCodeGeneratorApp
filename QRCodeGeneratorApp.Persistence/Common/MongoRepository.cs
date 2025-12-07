using MongoDB.Driver;

namespace QRCodeGeneratorApp.Persistence.Common
{
    public abstract class MongoRepository<T> where T : class
    {
        public const string DatabaseName = "qrcodegeneratorapp-db";

        protected readonly IMongoClient mongoClient;

        protected readonly IMongoCollection<T> dbCollection;

        protected abstract string collectionName { get; }

        protected MongoRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            this.dbCollection = this.mongoClient.GetDatabase(DatabaseName).GetCollection<T>(collectionName);
        }
    }
}
