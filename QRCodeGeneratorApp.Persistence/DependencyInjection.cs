using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QRCodeGeneratorApp.Persistence.Orders;
using QRGeneratorApp.Core.Orders;

namespace QRCodeGeneratorApp.Persistence
{
    public static class DependencyInjection
    {
        public static void RegisterPersistenceServices(this IServiceCollection services)
        {
           services.AddScoped<IOrdersRepository, OrdersRepository>();
        }

        public static void RegisterMongoDbClient(this IHostApplicationBuilder builder)
        {
            builder.AddMongoDBClient("qrcodegeneratorapp-db");
        }
    }
}
