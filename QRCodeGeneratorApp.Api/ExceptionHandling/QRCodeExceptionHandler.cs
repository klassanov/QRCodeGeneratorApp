using Microsoft.AspNetCore.Diagnostics;

namespace QRCodeGeneratorApp.Api.ExceptionHandling
{
    public class QRCodeExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //Simulate that this handler does not handle any exceptions
            return ValueTask.FromResult(false);
        }
    }
}
