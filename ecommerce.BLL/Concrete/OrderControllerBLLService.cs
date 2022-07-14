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

        public async Task<PageWrapper<CreditCard>> GetAllSavedCreditCards(int page, int perPage, int userId)
        {
            var cards = await this._orderControllerDALService.GetAllSavedCreditCards(userId);
            var totalCount = cards.Count;
            PageWrapper<CreditCard> pageList = new PageWrapper<CreditCard>
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

        public async Task<BasketDetails> GetItemsAddedToBasket(int userId)
        {
            BasketDetails basket = await this._orderControllerDALService.GetItemsAddedToBasket(userId);
          
            return basket;
        }

        public async Task<OrderProductDetails> GetOrderById(int userId, int orderId)
        {
            OrderProductDetails order = await this._orderControllerDALService.GetOrderById(userId,orderId);

            return order;
        }

        public async Task<PageWrapper<OrderProductDetails>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId, int page, int perPage)
        {
            var orders = await this._orderControllerDALService.GetOrdersByDate(dateFrom, dateTo, userId);
            var totalCount = orders.Count;
            PageWrapper<OrderProductDetails> pageList = new PageWrapper<OrderProductDetails>
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

        public async Task<bool> UpdateItemsInBasket(ItemsToAddRequestModel itemsRequestModel, int userId)
        {
            bool orderPlaced = await this._orderControllerDALService.UpdateItemsInBasket(itemsRequestModel, userId);

            return orderPlaced;
        }
    }
}
