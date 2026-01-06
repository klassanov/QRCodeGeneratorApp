using Microsoft.Extensions.DependencyInjection;
using QRCodeGeneratorApp.Infrastructure.Messaging.InMemory;
using QRGeneratorApp.Core.Common.Messaging;

namespace QRCodeGeneratorApp.Infrastructure
{
    public static class DependencyInjection
    {
        //Trying out the extension members feature of C# 14
        extension(IServiceCollection services)
        {
            public IServiceCollection RegisterInfrastructureServices() 
            {

                services.AddSingleton(typeof(IEventBus<>), typeof(InMemoryEventBus<>));

                return services;
            }
        }
    }
}
