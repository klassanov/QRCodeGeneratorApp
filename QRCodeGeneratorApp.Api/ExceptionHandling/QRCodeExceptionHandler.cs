using Microsoft.AspNetCore.Diagnostics;
using QRGeneratorApp.Core.Common.Exceptions;

namespace QRCodeGeneratorApp.Api.ExceptionHandling
{
    public class QRCodeExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //Simulate that this handler does not handle any exceptions

            if (exception is not QRCodeException qrCodeException)
            {
                //This handler does not handle this exception, return false to let the next handler try
                return ValueTask.FromResult(false);
            }


            // Continue handling QRCodeException here
            return ValueTask.FromResult(true);

        }
    }
}
