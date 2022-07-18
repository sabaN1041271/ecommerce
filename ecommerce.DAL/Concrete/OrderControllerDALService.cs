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
        ConnectionStringManager connectionStringManager;
        public Task<bool> AddCreditCardToAccount(string cardType, string cardNumber, int expiryMonth, int expiryYear, int cvc, int userId)
        {
            string result = string.Empty;
            try

            {
                connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

                SqlCommand cmd = new SqlCommand("ADD_CREDIT_CARD_TO_USER_ACCOUNT", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@cardType", cardType);
                cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
                cmd.Parameters.AddWithValue("@expiryMonth", expiryMonth);
                cmd.Parameters.AddWithValue("@expiryYear", expiryYear);
                cmd.Parameters.AddWithValue("@cvc", cvc);

                con.Open();
                result = cmd.ExecuteNonQuery().ToString();
               
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

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

                SqlCommand cmd = new SqlCommand("ADD_ITEMS_TO_BASKET", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@productId", orderRequestModel.itemDetails.FirstOrDefault().ProductId);
                cmd.Parameters.AddWithValue("@quantity", orderRequestModel.itemDetails.FirstOrDefault().Quantity);

                con.Open();
                result = cmd.ExecuteNonQuery().ToString();

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

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

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
                             ProductDetails = GetProducts(dt_products, Convert.ToInt32(dr["userId"]), Convert.ToInt32(dr["pkOrderId"]))
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

        private List<ProductsAdded> GetProducts(DataTable dt_products, int userId, int orderId = 0, int basketId = 0)
        {
            List<ProductsAdded> productsAddeds = new List<ProductsAdded>();
            if(orderId > 0)
            {
                 productsAddeds = (from DataRow dr in dt_products.Rows
                                where Convert.ToInt32(dr["userId"]) == userId && Convert.ToInt32(dr["orderId"]) == orderId
                                select new ProductsAdded()
                                {
                                    ProductId = Convert.ToInt32(dr["pkProductId"]),
                                    ProductName = dr["productName"].ToString(),
                                    ProductQuantity = Convert.ToInt32(dr["quantity"]),
                                    PriceOfSingleProduct = Convert.ToDouble(dr["price"]),
                                    TotalPriceOfProduct = Convert.ToInt32(dr["quantity"]) * Convert.ToDouble(dr["price"])
                                }).ToList();
            }
            else
            {
                productsAddeds = (from DataRow dr in dt_products.Rows
                                  where Convert.ToInt32(dr["userId"]) == userId && Convert.ToInt32(dr["basketId"]) == basketId
                                  select new ProductsAdded()
                                  {
                                      ProductId = Convert.ToInt32(dr["pkProductId"]),
                                      ProductName = dr["productName"].ToString(),
                                      ProductQuantity = Convert.ToInt32(dr["quantity"]),
                                      PriceOfSingleProduct = Convert.ToDouble(dr["price"]),
                                      TotalPriceOfProduct = Convert.ToInt32(dr["quantity"]) * Convert.ToDouble(dr["price"])
                                  }).ToList();
            }
           
            return productsAddeds;
        }

        public Task<List<CreditCard>> GetAllSavedCreditCards(int userId)
        {

            List<CreditCard> creditCards
                = new List<CreditCard>();
            try

            {

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

                SqlCommand cmd = new SqlCommand("Get_All_Saved_Credit_Cards", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataTable dt = new DataTable();

                da.Fill(dt);


                creditCards = (from DataRow dr in dt.Rows
                          select new CreditCard()
                          {
                              Id = Convert.ToInt32(dr["pkCreditCardId"]),
                              CardNumber =dr["cardNumber"].ToString(),
                              CardType = dr["cardType"].ToString(),
                              ExpiryMonth = Convert.ToInt16(dr["expMonth"]),
                              ExpiryYear = Convert.ToInt32(dr["expiryYear"])
                          }).ToList();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(creditCards);
        }

        public Task<BasketDetails> GetItemsAddedToBasket(int userId)
        {
            BasketDetails basket
                = new BasketDetails();
            try

            {

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

                SqlCommand cmd = new SqlCommand("Get_Items_In_Basket", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                da.SelectCommand = cmd;

                DataSet ds = new DataSet();

                da.Fill(ds);

                DataTable dt_basket = ds.Tables[0];
                DataTable dt_products = ds.Tables[1];


                basket = (from DataRow dr in dt_basket.Rows
                          select new BasketDetails()
                          {
                              BasketId = Convert.ToInt32(dr["pkBasketId"]),
                              UserId = Convert.ToInt32(dr["userId"]),
                              ProductsAddeds = GetProducts(dt_products, Convert.ToInt32(dr["userId"]), 0,Convert.ToInt32(dr["pkBasketId"]))
                          }).FirstOrDefault();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(basket);
        }

        public Task<OrderProductDetails> GetOrderById(int userId, int orderId)
        {
            OrderProductDetails order = new OrderProductDetails();
            try

            {

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

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

                order = (from DataRow dr in dt_orders.Rows
                         where (Convert.ToInt32(dr["pkOrderId"])) == orderId
                         select new OrderProductDetails()
                         {
                             OrderId = Convert.ToInt32(dr["pkOrderId"]),
                             OrderDate = Convert.ToDateTime(dr["orderDate"]),
                             ProductDetails = GetProducts(dt_products, Convert.ToInt32(dr["userId"]), Convert.ToInt32(dr["pkOrderId"]))
                         }).FirstOrDefault();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(order);
        }

        public Task<List<OrderProductDetails>> GetOrdersByDate(DateTime dateFrom, DateTime dateTo, int userId)
        {
            List<OrderProductDetails> orders = new List<OrderProductDetails>();
            try

            {

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

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
                          where Convert.ToDateTime(dr["orderDate"]) >= dateFrom && Convert.ToDateTime(dr["orderDate"]) <= dateTo
                          select new OrderProductDetails()
                          {
                              OrderId = Convert.ToInt32(dr["pkOrderId"]),
                              OrderDate = Convert.ToDateTime(dr["orderDate"]),
                              ProductDetails = GetProducts(dt_products, Convert.ToInt32(dr["userId"]), Convert.ToInt32(dr["pkOrderId"]))
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

        public Task<bool> PlaceOrder(OrderRequestModel orderRequestModel, int userId, int cardId)
        {
            bool updated = false;
            try

            {
                DataTable dt_products = new DataTable();
                dt_products.Clear();
                dt_products.Columns.Add("productId");
                dt_products.Columns.Add("quantity");
                
                if(orderRequestModel.itemDetails != null && orderRequestModel.itemDetails.Count > 0)
                {
                    foreach(var product in orderRequestModel.itemDetails)
                    {
                        DataRow dataRow = dt_products.NewRow();
                        dataRow["productId"] = product.ProductId;
                        dataRow["quantity"] = product.Quantity;
                        dt_products.Rows.Add(dataRow);

                    }
                }

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

                SqlCommand cmd = new SqlCommand("Place_Order", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@productDetails", dt_products);
                cmd.Parameters.AddWithValue("@cardId", cardId);

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    updated = true;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(updated);
        }

        public Task<bool> UpdateItemsInBasket(ItemsToAddRequestModel itemsRequestModel, int userId)
        {
            bool updated = false;
            try

            {

               connectionStringManager = new ConnectionStringManager();
                con = new SqlConnection(connectionStringManager.GetConnectionString());

                SqlCommand cmd = new SqlCommand("Update_Items_In_Basket", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@productId", itemsRequestModel.ProductId);
                cmd.Parameters.AddWithValue("@quantity", itemsRequestModel.Quantity);

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                if(rowsAffected > 0)
                {
                    updated = true;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(updated);
        }
    }
}
