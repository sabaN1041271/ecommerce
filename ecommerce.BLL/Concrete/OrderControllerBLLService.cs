using ecommerce.BLL.Abstract;
using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecommerce.DAL;
using ecommerce.DAL.Abstract;

namespace ecommerce.BLL.Concrete
{
    public class OrderControllerBLLService : IOrderControllerBLLService
    {
        private readonly IOrderControllerDALService _orderControllerDALService;
        public OrderControllerBLLService(IOrderControllerDALService orderControllerDALService)
        {
            this._orderControllerDALService = orderControllerDALService;
        }
        public async Task<bool> AddCreditCardToAccount(CreditCardRequestModel creditCardRequestModel, int userId)
        {
            bool result = await this._orderControllerDALService.AddCreditCardToAccount(creditCardRequestModel.CardType, creditCardRequestModel.CardNumber, creditCardRequestModel.ExpiryMonth, creditCardRequestModel.ExpiryYear, creditCardRequestModel.Cvc, userId);
            return result;
        }

        public async Task<bool> AddItemsToBasket(OrderRequestModel orderRequestModel, int userId)
        {
            bool result = await this._orderControllerDALService.AddItemsToBasket(orderRequestModel, userId);
            return result;
        }

        public async Task<PageWrapper<OrderProductDetails>> AllOrders(int page, int perPage, int userId)
        {
            var orders = await this._orderControllerDALService.AllOrders(userId);
            var totalCount = orders.Count;
            PageWrapper<OrderProductDetails> pageList = new PageWrapper<OrderProductDetails>
            {
                Items = orders.Skip((page - 1) * perPage)
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

        public async Task<PageWrapper<CreditCardResponseModel>> GetAllSavedCreditCards(int page, int perPage, int userId)
        {
            var cards = await this._orderControllerDALService.GetAllSavedCreditCards(userId);
            var totalCount = cards.Count;
            PageWrapper<CreditCardResponseModel> pageList = new PageWrapper<CreditCardResponseModel>
            {
                Items = cards.Skip((page - 1) * perPage)
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

        public async Task<BasketResponseModel> GetItemsAddedToBasket(int userId)
        {
            BasketResponseModel basket = await this._orderControllerDALService.GetItemsAddedToBasket(userId);
          
            return basket;
        }

        public async Task<OrderDetailResponseModel> GetOrderById(int userId, int orderId)
        {
            OrderDetailResponseModel order = await this._orderControllerDALService.GetOrderById(userId,orderId);

            return order;
        }

        public async Task<PageWrapper<OrderDetailResponseModel>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId, int page, int perPage)
        {
            var orders = await this._orderControllerDALService.GetOrdersByDate(dateFrom, dateTo, userId);
            var totalCount = orders.Count;
            PageWrapper<OrderDetailResponseModel> pageList = new PageWrapper<OrderDetailResponseModel>
            {
                Items = orders
                .Skip((page - 1) * perPage)
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


        public async Task<bool> PlaceOrder(OrderRequestModel orderRequestModel, int userId, int cardId)
        {
            bool orderPlaced = await this._orderControllerDALService.PlaceOrder(orderRequestModel,userId, cardId);

            return orderPlaced;
        }

        public async Task<bool> UpdateItemsInBasket(ItemsToUpdateRequestModel itemsRequestModel, int userId)
        {
            bool orderPlaced = await this._orderControllerDALService.UpdateItemsInBasket(itemsRequestModel, userId);

            return orderPlaced;
        }
    }
}
