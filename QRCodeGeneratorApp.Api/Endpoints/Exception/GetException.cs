using Carter;

namespace QRCodeGeneratorApp.Api.Endpoints.Exception
{
    public class GetException : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/exception/{exceptionType}", (string exceptionType) =>
            {
                var ex = exceptionType.ToLower() switch
                {
                    "application" => new ApplicationException("This is a test application exception from /exception endpoint"),
                    "invalidoperation" => new InvalidOperationException("This is a test invalid operation exception from /exception endpoint"),
                    _ => new System.Exception("This is a test exception from /exception endpoint")
                };

                throw ex;
            });
        }
    }
}
