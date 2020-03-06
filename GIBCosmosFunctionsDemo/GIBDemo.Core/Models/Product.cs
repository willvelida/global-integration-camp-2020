using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GIBDemo.Core.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public double Price { get; set; }
        public string Manufacturer { get; set; }

    }
}
