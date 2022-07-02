using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ecommerce.BLL;
using ecommerce.BLL.Abstract;
using ecommerce.Model;
using System.Threading.Tasks;

namespace ecommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsControllerBLLService _productControllerService;
        public ProductsController(IProductsControllerBLLService productControllerService)
        {
            this._productControllerService = productControllerService;
        }
        [HttpGet]
        [Route("products")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PageWrapper<ProductDetails>), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetProductList([FromQuery] int page, [FromQuery] int perPage)
        {


            PageWrapper<ProductDetails> response = await this._productControllerService.AllProducts(page, perPage);


            if (response == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }

        [HttpGet]
        [Route("products/{productId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductDetails), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetProductById([FromRoute] int productId)
        {

            ProductDetails response = await this._productControllerService.GetProductById(productId);


            if (response == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }
        [HttpPost]
        [Route("SearchByProductName")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PageWrapper<ProductDetails>), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> SearchByProductName(string query)
        {

            PageWrapper<ProductDetails> response = await this._productControllerService.GetProductByProductName(query);


            if (response == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }


    }
}
