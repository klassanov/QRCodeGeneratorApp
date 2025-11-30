using MongoDB.Driver;
using QRGeneratorApp.Core.ClientRequests;
using QRGeneratorApp.Core.ClientRequests.Get;

namespace QRCodeGeneratorApp.Persistence.ClientRequests
{
    public class ClientRequestsRepository : IClientRequestsRepository
    {
        private readonly IMongoClient mongoClient;

        public ClientRequestsRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public async Task<GetClientRequestResult> GetById(Guid id)
        {
            return new GetClientRequestResult(
                 Id: id,
                 ClientName: "Test Client",
                 RequestedText: "Test Text",
                 RequestTime: DateTime.UtcNow
             );
        }
    }
}
