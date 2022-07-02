using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class ProductDetails
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string ProductName { get; set; } = "";
        [JsonProperty]
        public string Brand { get; set; } = "";
        [JsonProperty]
        public string Avatar { get; set; } = "";
        [JsonProperty]
        public string Description { get; set; } = "";
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public double Price { get; set; } = 0.0;
        [JsonProperty]
        public DateTime ExpiryDate { get; set; }
    }
}
