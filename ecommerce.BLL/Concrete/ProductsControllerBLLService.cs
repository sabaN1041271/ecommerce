using ecommerce.BLL.Abstract;
using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Concrete
{
    public class ProductsControllerBLLService : IProductsControllerBLLService
    {
        public Task<PageWrapper<ProductDetails>> AllProducts(int page, int perPage)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetails> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<PageWrapper<ProductDetails>> GetProductByProductName(string query)
        {
            throw new NotImplementedException();
        }
    }
}
