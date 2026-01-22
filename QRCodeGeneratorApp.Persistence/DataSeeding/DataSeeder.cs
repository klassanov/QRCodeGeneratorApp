using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using QRCodeGeneratorApp.Persistence.Common;
using QRCodeGeneratorApp.Persistence.Orders;

namespace QRCodeGeneratorApp.Persistence.DataSeeding
{
    public static class DataSeeder
    {

        public static void SeedOrders(this IServiceProvider serviceProvider)
        {
            var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
            var database = mongoClient.GetDatabase(MongoRepository<OrderDocument>.DatabaseName);
            var ordersCollection = database.GetCollection<OrderDocument>("orders");
            var existingOrdersCount = ordersCollection.CountDocuments(FilterDefinition<OrderDocument>.Empty);

            if (existingOrdersCount == 0)
            {
                var initialOrders = new List<OrderDocument>
                {
                    new("QR Code Generator Pro", "Customer 1", "customer-1@test.com"),
                    new("QR Code Generator SIlver", "Customer 2", "customer-2@test.com"),
                    new("QR Code Generator Basic", "Customer 3", "customer-3@test.com"),
                };

                ordersCollection.InsertMany(initialOrders);
            }
        }
    }
}
