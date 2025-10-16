using Carter;
using Microsoft.AspNetCore.Mvc;
using QRGeneratorApp.Core.ClientRequests.Get;
using QRGeneratorApp.Core.Common.Mediator;

namespace QRCodeGeneratorApp.Api.Endpoints.ClientRequests
{
    public class GetClientRequest : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/clientrequests/{id:guid}", async (Guid id, [FromServices] IQueryHandler<GetClientRequestQuery, GetClientRequestResult> queryHandler) =>
            {
                var query = new GetClientRequestQuery(id);
                var result = await queryHandler.Handle(query);
                return Results.Ok(result);
            });
        }
    }
}
