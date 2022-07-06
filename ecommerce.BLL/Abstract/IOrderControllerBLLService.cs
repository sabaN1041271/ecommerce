using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Abstract
{
    public interface IOrderControllerBLLService
    {
        Task<PageWrapper<OrderProductDetails>> AllOrders(int page, int perPage, int userId);
        Task<OrderDetailResponseModel> GetOrderById(int userId, int orderId);
        Task<bool> AddCreditCardToAccount(CreditCardRequestModel creditCardRequestModel, int userId);
        Task<PageWrapper<CreditCardResponseModel>> GetAllSavedCreditCards(int page, int perPage, int userId);
        Task<bool> PlaceOrder(OrderRequestModel orderRequestModel, int userId, int cardId);
        Task<PageWrapper<OrderDetailResponseModel>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId, int page, int perPage);
        Task<bool> AddItemsToBasket(OrderRequestModel orderRequestModel, int userId);
        Task<BasketResponseModel> GetItemsAddedToBasket(int userId);
        Task<bool> UpdateItemsInBasket(ItemsToUpdateRequestModel itemsRequestModel, int userId);
    }
}
