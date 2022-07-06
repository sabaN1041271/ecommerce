using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DAL.Abstract
{
    public interface IOrderControllerDALService
    {
        Task<bool> AddCreditCardToAccount(string cardType, string cardNumber, int expiryMonth, int expiryYear, int cvc, int userId);
        Task<bool> AddItemsToBasket(OrderRequestModel orderRequestModel, int userId);
        Task<List<OrderProductDetails>> AllOrders(int userId);
        Task<List<CreditCardResponseModel>> GetAllSavedCreditCards(int userId);
        Task<BasketResponseModel> GetItemsAddedToBasket(int userId);
        Task<OrderDetailResponseModel> GetOrderById(int userId, int orderId);
        Task<List<OrderDetailResponseModel>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId);
        Task<bool> PlaceOrder(OrderRequestModel orderRequestModel, int userId, int cardId);
        Task<bool> UpdateItemsInBasket(ItemsToUpdateRequestModel itemsRequestModel, int userId);
    }
}
