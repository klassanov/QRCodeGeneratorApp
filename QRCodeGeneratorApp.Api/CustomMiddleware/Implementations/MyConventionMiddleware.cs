using QRCodeGeneratorApp.Api.CustomMiddleware.CustomMiddlewareServices;

namespace QRCodeGeneratorApp.Api.CustomMiddleware.Implementations
{
    //Middleware by convention
    //Registered as Singleton, so in order to obtain the scoped service, we can inject it into the invoke async method
    public class MyConventionMiddleware
    {
        private readonly RequestDelegate next;

        //Constructor called only once in the beginning
        public MyConventionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        //In order to obtain the scoped service, we can inject it into the invoke async method
        //or obtain it from the HttpContext.RequestServices
        public async Task InvokeAsync(HttpContext context, IMyCustomMiddlewareScopedService service)
        {
            // context.RequestServices.GetRequiredService<IMyCustomMiddlewareScopedService>();

            await next(context);
        }

    }
}
