using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DAL.Abstract
{
    public interface IProductsControllerDALService
    {
        Task<List<ProductDetails>> AllProducts();
        Task<ProductDetails> GetProductById(int productId);
        Task<List<ProductDetails>> GetProductByProductName(string query);
    }
}
