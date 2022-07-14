using ecommerce.BLL.Abstract;
using ecommerce.DAL.Abstract;
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
        private readonly IProductsControllerDALService _productControllerDALService;
        public ProductsControllerBLLService(IProductsControllerDALService productControllerDALService)
        {
            this._productControllerDALService = productControllerDALService;
        }
        public async Task<PageWrapper<ProductDetails>> AllProducts(int page, int perPage)
        {
            List<ProductDetails> products = await this._productControllerDALService.AllProducts();
            var totalCount = products.Count;
            PageWrapper<ProductDetails> pageList = new PageWrapper<ProductDetails>
            {
                Items = products.Skip((page - 1) * perPage)
                .Take(perPage).ToList(),
                PaginationInfo = new PaginationInfo
                {
                    Count = Convert.ToInt32(totalCount),
                    Page = page,
                    PerPage = perPage
                }

            };
            return pageList;
        }

        public async Task<ProductDetails> GetProductById(int productId)
        {
            ProductDetails product = await this._productControllerDALService.GetProductById(productId);

            return product;
        }

        public async Task<PageWrapper<ProductDetails>> GetProductByProductName(string query, int page = 1, int perPage = int.MaxValue)
        {
            List<ProductDetails> products = await this._productControllerDALService.GetProductByProductName(query);

            var totalCount = products.Count;
            PageWrapper<ProductDetails> pageList = new PageWrapper<ProductDetails>
            {
                Items = products.Skip((page - 1) * perPage)
                .Take(perPage).ToList(),
                PaginationInfo = new PaginationInfo
                {
                    Count = Convert.ToInt32(totalCount),
                    Page = page,
                    PerPage = perPage
                }

            };
            return pageList;
        }
    }
}
