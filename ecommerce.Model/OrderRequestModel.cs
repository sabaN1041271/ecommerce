using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class OrderRequestModel
    {
        [JsonProperty]
        public List<ItemsToAddRequestModel> itemDetails { get; set; }
    }

    public class ItemsToAddRequestModel
    {
        [JsonProperty]
        public int ProductId { get; set; }
        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public double Price { get; set; }
    }
}
