using MongoDB.Driver;
using QRGeneratorApp.Core.Orders;
using QRGeneratorApp.Core.Orders.GetById;

namespace QRCodeGeneratorApp.Persistence.ClientRequests
{
    public class ClientRequestsRepository : IOrdersRepository
    {
        private readonly IMongoClient mongoClient;

        public ClientRequestsRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public async Task<GetOrderByIdResult> GetById(Guid id)
        {
            var dbList = await mongoClient.ListDatabaseNamesAsync();
            foreach (var db in await dbList.ToListAsync())
            {
                Console.WriteLine(db);
            }

            

                return await Task.FromResult( new GetOrderByIdResult(
                 Id: id,
                 ClientName: "Test Client",
                 RequestedText: "Test Text",
                 RequestTime: DateTime.UtcNow
             ));
        }
    }
}
