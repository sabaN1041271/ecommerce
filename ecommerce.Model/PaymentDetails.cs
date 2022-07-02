using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class PaymentDetails
    {

        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public int CardId { get; set; }
        [JsonProperty]
        public int OrderId { get; set; }
        [JsonProperty]
        public DateTime PaymentDate { get; set; }
        [JsonProperty]
        public PaymentStatusEnum PaymentStatus { get; set; }
    }

    public enum PaymentStatusEnum
    {
        successful,
        failed
    }
}
