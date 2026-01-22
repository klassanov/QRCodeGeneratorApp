using QRCodeGeneratorApp.Api.CustomMiddleware.CustomMiddlewareServices;

namespace QRCodeGeneratorApp.Api.CustomMiddleware.Implementations
{
    public static class InlineMiddleware
    {
        public static IApplicationBuilder AddInlineMiddleware(this IApplicationBuilder app)
        {
            //Inline middleware
            app.Use(async (context, next) =>
            {
                //Obtain scoped service from the HttpContext.RequestServices
                var service = context.RequestServices.GetRequiredService<IMyCustomMiddlewareScopedService>();

                //Custom inline middleware logic before
                await next.Invoke();
                //Custom inline middleware logic after
            });

            return app;
        }
    }
}
