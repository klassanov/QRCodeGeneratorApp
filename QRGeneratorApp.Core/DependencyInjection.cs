using Microsoft.Extensions.DependencyInjection;
using QRGeneratorApp.Core.GridCreation;
using QRGeneratorApp.Core.QRMapCreation;

namespace QRGeneratorApp.Core
{
    public static class DependencyInjection
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IGridCreator, GridCreator>();
            services.AddTransient<IQRMapCreator, QRMapCreator>();
        }
    }
}
