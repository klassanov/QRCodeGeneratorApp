using QRCodeGeneratorApp.Api.CustomMiddleware.CustomMiddlewareServices;
using QRCodeGeneratorApp.Api.CustomMiddleware.Implementations;

namespace QRCodeGeneratorApp.Api.CustomMiddleware
{
    public static class CustomMiddlewareExtensions
    {
        public static IServiceCollection AddCustomMiddleware(this IServiceCollection services)
        {
            //Register service used in all middlewares
            services.AddScoped<IMyCustomMiddlewareScopedService, MyCustomMiddlewareScopedService>();

            //Register MyScopedMiddleware as scoped
            services.AddScoped<MyScopedMiddleware>();

            return services;
        }



        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            //Factory middleware
            app.UseMiddleware<MyScopedMiddleware>();

            //Convention middleware
            app.UseMiddleware<MyConventionMiddleware>();

            //Inline middleware
            app.AddInlineMiddleware();

            return app;
        }
    }
}
