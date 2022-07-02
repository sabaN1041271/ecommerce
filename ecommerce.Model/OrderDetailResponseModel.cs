using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class OrderDetailResponseModel
    {
        [JsonProperty]
        public List<OrderDetails> orderDetailsList { get; set; }
    }
}
