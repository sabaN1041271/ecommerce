using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class UserDetails
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string FirstName { get; set; } = "";
        [JsonProperty]
        public string LastName { get; set; } = "";
        [JsonProperty]
        public string Email { get; set; } = "";
        [JsonProperty]
        public string Password { get; set; } = "";
        [JsonProperty]
        public string MobileNumber { get; set; } = "";
        [JsonProperty]
        public string gender { get; set; } = "";
        [JsonProperty]
        public List<AddressDetails> Addresses { get; set; }

    }

    public class AddressDetails
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string Address { get; set; }
        [JsonProperty]
        public int UserId { get; set; }
    }
}
