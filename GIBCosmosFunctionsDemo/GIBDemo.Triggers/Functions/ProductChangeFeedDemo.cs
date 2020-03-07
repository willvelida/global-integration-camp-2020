using System;
using System.Collections.Generic;
using GIBDemo.Core.Helpers;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GIBDemo.Triggers.Functions
{
    public static class ProductChangeFeedDemo
    {
        // Provide your own URI and keys
        private static DocumentClient _documentClient = new DocumentClient(new Uri(""), "");

        [FunctionName(nameof(ProductChangeFeedDemo))]
        public static void Run([CosmosDBTrigger(
            databaseName: Constants.COSMOS_DB_DATABASE_NAME,
            collectionName: Constants.COSMOS_DB_CONTAINER_NAME,
            ConnectionStringSetting = "cosmosConnection",
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

                // For each document in our list of documents
                foreach (var document in input)
                {
                    // try to persist to our backup collection!
                    try
                    {
                        _documentClient.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri("Products", "ProductBackup"),
                        document);
                        log.LogInformation($"Document with {document.Id} inserted");
                    }
                    catch (Exception ex)
                    {
                        log.LogError($"Something went wrong. Exception thrown: {ex.Message}");
                        throw;
                    }
                    
                }
            }
        }
    }
}
