using ecommerce.DAL.Abstract;
using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DAL.Concrete
{
    public class OrderControllerDALService : IOrderControllerDALService
    {
        SqlConnection con;
        public Task<bool> AddCreditCardToAccount(string cardType, string cardNumber, int expiryMonth, int expiryYear, int cvc, int userId)
        {
            string result = string.Empty;
            try

            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                SqlCommand cmd = new SqlCommand("ADD_CREDIT_CARD_TO_USER_ACCOUNT", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@cardType", cardType);
                cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
                cmd.Parameters.AddWithValue("@expiryMonth", expiryMonth);
                cmd.Parameters.AddWithValue("@expiryYear", expiryYear);
                cmd.Parameters.AddWithValue("@cvc", cvc);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
               
            }
            catch(Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            if (result != "0")
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> AddItemsToBasket(OrderRequestModel orderRequestModel, int userId)
        {
            string result = string.Empty;
            try

            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                SqlCommand cmd = new SqlCommand("ADD_ITEMS_TO_BASKET", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@productId", orderRequestModel.itemDetails.FirstOrDefault().ProductId);
                cmd.Parameters.AddWithValue("@quantity", orderRequestModel.itemDetails.FirstOrDefault().Quantity);

                con.Open();
                result = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            if (result != "0")
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<List<OrderProductDetails>> AllOrders(int userId)
        {
            List<OrderProductDetails> orders = new List<OrderProductDetails>();
            try

            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                SqlCommand cmd = new SqlCommand("Get_All_Orders", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataSet ds = new DataSet();

                da.Fill(ds);

                DataTable dt_orders = ds.Tables[0];
                DataTable dt_products = ds.Tables[1];

                orders = (from DataRow dr in dt_orders.Rows
                         select new OrderProductDetails()
                         {
                             OrderId = Convert.ToInt32(dr["pkOrderId"]),
                             OrderDate = Convert.ToDateTime(dr["orderDate"]),
                             ProductDetails = GetOrderProducts(dt_products, Convert.ToInt32(dr["pkUserId"]), Convert.ToInt32(dr["pkOrderId"]))
                         }).ToList();

                          
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(orders);
        }

        private List<ProductsAdded> GetOrderProducts(DataTable dt_products, int userId, int orderId)
        {
            var products = (from DataRow dr in dt_products.Rows
                            where Convert.ToInt32(dr["userId"]) == userId && Convert.ToInt32(dr["orderId"]) == orderId
                            select new ProductsAdded()
                             {
                                 ProductId = Convert.ToInt32(dr["pkProductId"]),
                                 ProductName = dr["productName"].ToString(),
                                 ProductQuantity = Convert.ToInt32(dr["quantity"]),
                                 PriceOfSingleProduct = Convert.ToDouble(dr["price"]),
                                 TotalPriceOfProduct = Convert.ToInt32(dr["quantity"]) * Convert.ToDouble(dr["price"])
                            }).ToList();
            return products;
        }

        public Task<List<CreditCardResponseModel>> GetAllSavedCreditCards(int userId)
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

        public Task<List<OrderDetailResponseModel>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId)
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
