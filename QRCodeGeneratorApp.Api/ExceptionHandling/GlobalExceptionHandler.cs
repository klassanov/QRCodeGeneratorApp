using Microsoft.AspNetCore.Diagnostics;

namespace QRCodeGeneratorApp.Api.ExceptionHandling
{


    /// <summary>
    /// ASP.NET Core 8 introduced IExceptionHandler interface, and it's a game-changer.
    /// Instead of one massive middleware handling everything, we can create focused handlers for specific exception types.
    /// The key here is the return value. If your handler can deal with the exception, return true. 
    /// If not, return false and let the next handler try
    /// You can chain multiple exception handlers together, and they'll run 
    /// in the order you register them. ASP.NET Core will use the first one that returns true from TryHandleAsync.
    /// </summary>

    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService problemDetailsService;
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(
            IProblemDetailsService problemDetailsService,
            ILogger<GlobalExceptionHandler> logger)
        {
            this.problemDetailsService = problemDetailsService;
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //This is the catch-all handler, that is last in the chain

            logger.LogError(exception, "An unhandled exception occurred.");

            //Optionally decide the status code based on exception type
            httpContext.Response.StatusCode = exception switch
            {
                ApplicationException _ => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };


            //With try write we return a boolen indicating whether we handled the exception
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new()
                {
                    Type = exception.GetType().Name,
                    Title = "An unexpected error occurred!",
                    Detail = exception.Message,
                    //Status = StatusCodes.Status500InternalServerError,
                    Instance = httpContext.Request.Path
                }
            });
        }
    }
}
