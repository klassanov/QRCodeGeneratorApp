using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QRCodeGeneratorApp.Persistence.ClientRequests;
using QRGeneratorApp.Core.ClientRequests;

namespace QRCodeGeneratorApp.Persistence
{
    public static class DependencyInjection
    {
        public static void RegisterPersistenceServices(this IServiceCollection services)
        {
           services.AddScoped<IClientRequestsRepository, ClientRequestsRepository>();
        }

        public static void RegisterMongoDbClient(this IHostApplicationBuilder builder)
        {
            builder.AddMongoDBClient("qrcodegeneratorapp-db");
        }
    }
}
