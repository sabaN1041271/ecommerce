using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class ItemsToUpdateRequestModel
    {
        [JsonProperty]
        public List<ItemsToAddRequestModel> itemDetails { get; set; }
    }
}
