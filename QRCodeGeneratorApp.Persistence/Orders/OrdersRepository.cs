using MongoDB.Bson;
using MongoDB.Driver;
using QRGeneratorApp.Core.Orders;
using QRGeneratorApp.Core.Orders.GetById;

namespace QRCodeGeneratorApp.Persistence.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoClient mongoClient;

        public OrdersRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public async Task<GetOrderByIdResult> GetById(string id)
        {
            if(!ObjectId.TryParse(id, out var orderId)) 
                return null;

            var db = mongoClient.GetDatabase("qrcodegeneratorapp-db");
            var collection = db.GetCollection<OrderDocument>("orders");

            var order = await collection.Find(Builders<OrderDocument>.Filter.Eq(doc => doc.Id, orderId)).SingleOrDefaultAsync();

            if (order is null)
                return null;

            return new GetOrderByIdResult(id, order.CustomerName!, order.Text!, order.CreatedAt);
        }
    }
}
