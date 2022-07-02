using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class LoginUserDetailResponseModel
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public string FirstName { get; set; } 
        [JsonProperty]
        public string LastName { get; set; } 
        [JsonProperty]
        public string Token { get; set; } 
    }
}
