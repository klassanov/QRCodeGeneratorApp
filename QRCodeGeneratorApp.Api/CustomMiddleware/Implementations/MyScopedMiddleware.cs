using QRCodeGeneratorApp.Api.CustomMiddleware.CustomMiddlewareServices;

namespace QRCodeGeneratorApp.Api.CustomMiddleware.Implementations
{
    //AKA Factory middleware
    public class MyScopedMiddleware : IMiddleware
    {
        //Registered as scoped in Program.cs = > constructor called once per scope (per request), so we can resolve a scoped service
        //also in the constructor

        private readonly IMyCustomMiddlewareScopedService service;

        //Constructor called per each request
        public MyScopedMiddleware(IMyCustomMiddlewareScopedService service)
        {
            this.service = service;
        }


        //InvokeAsync is called per each request => we can resolve a scoped service inside InvokeAsync
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //Shall be the same instance as "service" from constructor
            var service2 = context.RequestServices.GetRequiredService<IMyCustomMiddlewareScopedService>();

            var areTheSame = service.GetInstanceId() == service2.GetInstanceId();


            await next(context);
        }
    }
}
