using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class CreditCardResponseModel
    {
        [JsonProperty]
        public List<CreditCard> creditCardList { get; set; }
    }

    public class CreditCard
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string CardType { get; set; } = "";
        [JsonProperty]
        public string CardNumber { get; set; } = "";
        [JsonProperty]
        public int ExpiryMonth { get; set; }
        [JsonProperty]
        public int ExpiryYear { get; set; }
       
    }
}
