using MongoDB.Bson;
using MongoDB.Driver;
using QRCodeGeneratorApp.Persistence.Common;
using QRGeneratorApp.Core.Orders;
using QRGeneratorApp.Core.Orders.GetById;

namespace QRCodeGeneratorApp.Persistence.Orders
{
    public class OrdersRepository : MongoRepository<OrderDocument>, IOrdersRepository
    {
        protected override string collectionName => "orders";
        
        public OrdersRepository(IMongoClient mongoClient): base(mongoClient) { }

        public async Task<GetOrderByIdResult?> GetById(string id)
        {
            if(!ObjectId.TryParse(id, out var orderId)) 
                return null;

            var order = await dbCollection.Find(Builders<OrderDocument>.Filter.Eq(doc => doc.Id, orderId)).SingleOrDefaultAsync();

            if (order is null)
                return null;

            return order.ToGetOrderByIdResult();
        }
    }
}
