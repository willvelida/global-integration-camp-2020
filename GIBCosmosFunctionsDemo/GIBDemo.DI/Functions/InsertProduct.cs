using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using GIBDemo.DI.Helpers;
using GIBDemo.Core.Models;

namespace GIBDemo.DI.Functions
{
    public class InsertProduct
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private CosmosClient _cosmosClient;

        private Database _database;
        private Container _container;

        public InsertProduct(
            ILogger<InsertProduct> logger,
            IConfiguration config,
            CosmosClient cosmosClient
            )
        {
            _logger = logger;
            _config = config;
            _cosmosClient = cosmosClient;

            _database = _cosmosClient.GetDatabase(_config[Constants.COSMOS_DB_DATABASE_NAME]);
            _container = _database.GetContainer(_config[Constants.COSMOS_DB_CONTAINER_NAME]);
        }

        [FunctionName(nameof(InsertProduct))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InsertProduct")] HttpRequest req)
        {
            IActionResult returnValue = null;

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var input = JsonConvert.DeserializeObject<Product>(requestBody);

                var product = new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = input.ProductName,
                    ProductType = input.ProductType,
                    Manufacturer = input.Manufacturer,
                    Price = input.Price
                };

                ItemResponse<Product> item = await _container.CreateItemAsync(
                    product,
                    new PartitionKey(product.ProductType));

                _logger.LogInformation("Item inserted");
                _logger.LogInformation($"This query cost: {item.RequestCharge} RU/s");
                returnValue = new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not insert item. Exception thrown: {ex.Message}");
                returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return returnValue;
        }
    }
}
