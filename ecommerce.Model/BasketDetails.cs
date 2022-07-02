using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Model
{
    public class BasketDetails
    {
        public int BasketId { get; set; }
        public List<ProductsAdded>  ProductsAddeds { get; set; }
        public int UserId { get; set; }
    }
}
