using MongoDB.Driver;

namespace QRCodeGeneratorApp.Persistence.Common
{
    public abstract class MongoRepository<T> where T : class
    {
        protected const string dbName = "qrcodegeneratorapp-db";

        protected readonly IMongoClient mongoClient;

        protected readonly IMongoCollection<T> dbCollection;

        protected abstract string collectionName { get; }

        protected MongoRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            this.dbCollection = this.mongoClient.GetDatabase(dbName).GetCollection<T>(collectionName);
        }
    }
}
