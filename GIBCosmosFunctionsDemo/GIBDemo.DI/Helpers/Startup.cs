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
                // Provide your own primary connection key
                CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder("");

                return cosmosClientBuilder.WithConnectionModeDirect()
                    .WithApplicationRegion("Australia East")
                    .WithBulkExecution(true)
                    .Build();
            });
        }
    }
}
