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
        private readonly IOrderControllerDALService orderControllerDALService;
        public Task<bool> AddCreditCardToAccount(CreditCardRequestModel creditCardRequestModel, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddItemsToBasket(OrderRequestModel orderRequestModel, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<PageWrapper<OrderDetailResponseModel>> AllOrders(int page, int perPage, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<PageWrapper<CreditCardResponseModel>> GetAllSavedCreditCards(int page, int perPage, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<BasketResponseModel> GetItemsAddedToBasket(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailResponseModel> GetOrderById(int userId, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<PageWrapper<OrderDetailResponseModel>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PlaceOrder(OrderRequestModel orderRequestModel, int userId, int cardId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemsInBasket(ItemsToUpdateRequestModel itemsRequestModel, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
