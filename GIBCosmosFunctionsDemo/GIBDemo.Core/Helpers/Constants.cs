using System;
using System.Collections.Generic;
using System.Text;

namespace GIBDemo.Core.Helpers
{
    public class Constants
    {
        public const string COSMOS_DB_CONNECTION_STRING = "AccountEndpoint=https://velidacosmosdb.documents.azure.com:443/;AccountKey=1MP1FMjkTZcb6Tqz929up9GWp2Tg42wCtKoQdTKxF03Xfq7ETSFUTStQY8UoquHB5w1qevGGSorAuPeEy7E3Tg==;";
        public const string COSMOS_DB_DATABASE_NAME = "Products";
        public const string COSMOS_DB_CONTAINER_NAME = "Product";
        public const string COSMOS_DB_LEASE_CONTAINER_NAME = "lease";
        public const string COSMOS_DB_BACKUP_CONTAINER = "ProductBackup";
        public const string COSMOS_DB_ACCOUNT_KEY = "1MP1FMjkTZcb6Tqz929up9GWp2Tg42wCtKoQdTKxF03Xfq7ETSFUTStQY8UoquHB5w1qevGGSorAuPeEy7E3Tg==";
    }
}
