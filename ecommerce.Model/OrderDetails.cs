using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class OrderProductDetails
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public int OrderId { get; set; }
        [JsonProperty]
        public DateTime OrderDate { get; set; }
        [JsonProperty]
        public List<ProductsAdded> ProductDetails { get; set; }      

    }

    public class ProductsAdded
    {
        [JsonProperty]
        public int ProductId { get; set; }
        [JsonProperty]
        public string ProductName { get; set; }
        [JsonProperty]
        public int ProductQuantity { get; set; }
        [JsonProperty]
        public double PriceOfSingleProduct { get; set; }
        [JsonProperty]
        public double TotalPriceOfProduct { get; set; }
    }

    public class OrderDetails
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public int UserId { get; set; }
        [JsonProperty]
        public DateTime OrderDate { get; set; }

        public OrderProductDetails OrderProductDetails { get; set; }

    }
}
