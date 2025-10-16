using QRGeneratorApp.Core.Common.Mediator;

namespace QRGeneratorApp.Core.ClientRequests.Get
{

    public record class GetClientRequestResult(
        Guid Id,
        string ClientName,
        string RequestedText,
        DateTime RequestTime
    );


    public record class GetClientRequestQuery(Guid Id) : IQuery<GetClientRequestResult>;


    public class GetGlientRequestHandler : IQueryHandler<GetClientRequestQuery, GetClientRequestResult>
    {
        public Task<GetClientRequestResult> Handle(GetClientRequestQuery query)
        {
            return Task.FromResult(new GetClientRequestResult(
                Id: query.Id,
                ClientName: "Test Client",
                RequestedText: "Test Text",
                RequestTime: DateTime.UtcNow
            ));
        }
    }
}
