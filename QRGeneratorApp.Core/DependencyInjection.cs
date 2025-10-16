using Microsoft.Extensions.DependencyInjection;
using QRGeneratorApp.Core.ClientRequests.Get;
using QRGeneratorApp.Core.Common.Mediator;
using QRGeneratorApp.Core.GridCreation;
using QRGeneratorApp.Core.QRMapCreation;

namespace QRGeneratorApp.Core
{
    public static class DependencyInjection
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddSingleton(new GridConfig());
            services.AddTransient<IGridCreator, GridCreator>();
            services.AddTransient<IQRMapCreator, QRMapCreator>();

            services.AddScoped<IQueryHandler<GetClientRequestQuery, GetClientRequestResult>, GetGlientRequestHandler>();
        }
    }
}
