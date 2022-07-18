using ecommerce.BLL.Abstract;
using ecommerce.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ecommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderControllerBLLService _orderControllerService;
        public OrderController(IOrderControllerBLLService orderControllerService)
        {
            this._orderControllerService = orderControllerService;
        }
        [HttpGet]
        [Route("orders")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PageWrapper<OrderResponseModel>), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetOrderList([FromQuery] int page, [FromQuery] int perPage, [FromQuery] int userId)
        {


            PageWrapper<OrderProductDetails> response = await this._orderControllerService.AllOrders(page, perPage, userId);


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
        [Route("orders/{orderId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(OrderDetailResponseModel), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetOrderById([FromRoute] int orderId, [FromQuery] int userId)
        {

            OrderProductDetails response = await this._orderControllerService.GetOrderById(userId,orderId);


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
        [Route("SearchByOrderDate")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PageWrapper<OrderDetailResponseModel>), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> SearchByOrderDate(DateTime dateFrom, DateTime dateTo, int userId, int page, int perPage)
        {

            PageWrapper<OrderProductDetails> response = await this._orderControllerService.GetOrdersByDate(dateFrom, dateTo, userId,page, perPage);


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
        [Route("AddItemsToBasket")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> AddItemsToBasket([FromBody] OrderRequestModel orderRequestModel, int userId)
        {

            bool response = await this._orderControllerService.AddItemsToBasket(orderRequestModel, userId);


            if (response == false)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }
        [HttpGet]
        [Route("GetItemsAddedToBasket")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasketResponseModel), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetItemsAddedToBasket(int userId)
        {

            BasketDetails response = await this._orderControllerService.GetItemsAddedToBasket(userId);


            if (response == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }

        [HttpPut]
        [Route("UpdateItemsInBasket")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> UpdateItemsInBasket(ItemsToAddRequestModel itemsRequestModel, int userId)
        {

            bool response = await this._orderControllerService.UpdateItemsInBasket(itemsRequestModel, userId);


            if (response == false)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }

        [HttpPost]
        [Route("AddCreditCard")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> AddCreditCardToAccount(CreditCardRequestModel creditCardRequestModel, int userId)
        {

            bool response = await this._orderControllerService.AddCreditCardToAccount(creditCardRequestModel, userId);


            if (response == false)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }

        [HttpGet]
        [Route("GetAllSavedCreditCards")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PageWrapper<CreditCardResponseModel>), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetAllSavedCreditCards([FromQuery] int page, [FromQuery] int perPage, [FromQuery] int userId)
        {

            PageWrapper<CreditCard> response = await this._orderControllerService.GetAllSavedCreditCards(page, perPage, userId);


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
        [Route("PlaceOrder")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> PlaceOrder(OrderRequestModel orderRequestModel, int userId, int cardId)
        {

            bool response = await this._orderControllerService.PlaceOrder(orderRequestModel, userId, cardId);


            if (response == false)
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
