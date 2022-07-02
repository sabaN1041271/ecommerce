using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Abstract
{
    public interface IProductsControllerBLLService
    {
        Task<PageWrapper<ProductDetails>> AllProducts(int page, int perPage);
        Task<ProductDetails> GetProductById(int productId);
        Task<PageWrapper<ProductDetails>> GetProductByProductName(string query);
    }
}
