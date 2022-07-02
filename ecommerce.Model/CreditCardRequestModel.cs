using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class CreditCardRequestModel
    {
        [Required]
        [StringLength(10)]
        public string CardType { get; set; }
        [Required]
        [StringLength(30)]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; } = "";
        [Required]
        public int ExpiryMonth { get; set; }
        [Required]
        public int ExpiryYear { get; set; }
        [Required]
        public int Cvc { get; set; }
    }
}
