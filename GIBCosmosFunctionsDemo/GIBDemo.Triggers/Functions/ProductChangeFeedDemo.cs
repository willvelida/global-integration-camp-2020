using System;
using System.Collections.Generic;
using GIBDemo.Core.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GIBDemo.Triggers.Functions
{
    public static class ProductChangeFeedDemo
    {
        [FunctionName(nameof(ProductChangeFeedDemo))]
        public static void Run([CosmosDBTrigger(
            databaseName: Constants.COSMOS_DB_DATABASE_NAME,
            collectionName: Constants.COSMOS_DB_CONTAINER_NAME,
            ConnectionStringSetting = Constants.COSMOS_DB_CONNECTION_STRING,
            LeaseCollectionName = Constants.COSMOS_DB_LEASE_CONTAINER_NAME,
            CreateLeaseCollectionIfNotExists = true,
            StartFromBeginning = true,
            FeedPollDelay = 5,
            PreferredLocations = "Australia East")]IReadOnlyList<Document> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
