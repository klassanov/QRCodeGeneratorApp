using QRGeneratorApp.Core.Common.Mediator;

namespace QRGeneratorApp.Core.Orders.GetById
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
    {
        private readonly IOrdersRepository clientRequestsRepository;

        public GetOrderByIdQueryHandler(IOrdersRepository clientRequestsRepository)
        {
            this.clientRequestsRepository = clientRequestsRepository;
        }

        public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query)
        {

            return await clientRequestsRepository.GetById(query.Id);

            //return Task.FromResult(new GetClientRequestResult(
            //    Id: query.Id,
            //    ClientName: "Test Client",
            //    RequestedText: "Test Text",
            //    RequestTime: DateTime.UtcNow
            //));
        }
    }
}
