using GIBDemo.Core.Helpers;
using GIBDemo.DI.Helpers;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace GIBDemo.DI.Helpers
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFilter(level => true);
            });

            var config = (IConfiguration)builder.Services.First(d => d.ServiceType == typeof(IConfiguration)).ImplementationInstance;

            builder.Services.AddSingleton((s) =>
            {
                CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder("AccountEndpoint=https://velidacosmosdb.documents.azure.com:443/;AccountKey=1MP1FMjkTZcb6Tqz929up9GWp2Tg42wCtKoQdTKxF03Xfq7ETSFUTStQY8UoquHB5w1qevGGSorAuPeEy7E3Tg==");

                return cosmosClientBuilder.WithConnectionModeDirect()
                    .WithApplicationRegion("Australia East")
                    .WithBulkExecution(true)
                    .Build();
            });
        }
    }
}
