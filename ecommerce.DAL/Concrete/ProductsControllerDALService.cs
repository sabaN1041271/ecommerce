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
    public class ProductsControllerDALService : IProductsControllerDALService
    {
        SqlConnection con;
        public Task<List<ProductDetails>> AllProducts()
        {
            List<ProductDetails> products = new List<ProductDetails>();
            try

            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                SqlCommand cmd = new SqlCommand("Get_All_Products", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataSet ds = new DataSet();

                da.Fill(ds);

                DataTable dt_products = ds.Tables[0];

                products = (from DataRow dr in dt_products.Rows
                          select new ProductDetails()
                          {
                              Id = Convert.ToInt32(dr["pkProductId"]),
                              Avatar = dr["avatar"].ToString(),
                              Description = dr["description"].ToString(),
                              Brand = dr["brand"].ToString(),
                              ExpiryDate = Convert.ToDateTime(dr["expiryDate"]),
                              Price = Convert.ToDouble(dr["price"]),
                              Quantity = Convert.ToInt32(dr["quantity"]),
                              ProductName = dr["productName"].ToString()                            
                          }).ToList();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(products);
        }

        public Task<ProductDetails> GetProductById(int productId)
        {
            ProductDetails product = new ProductDetails();
            try

            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                SqlCommand cmd = new SqlCommand("Get_All_Products", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataSet ds = new DataSet();

                da.Fill(ds);

                DataTable dt_products = ds.Tables[0];

                product = (from DataRow dr in dt_products.Rows
                            where Convert.ToInt32(dr["pkProductId"]) == productId
                            select new ProductDetails()
                            {
                                Id = Convert.ToInt32(dr["pkProductId"]),
                                Avatar = dr["avatar"].ToString(),
                                Description = dr["description"].ToString(),
                                Brand = dr["brand"].ToString(),
                                ExpiryDate = Convert.ToDateTime(dr["expiryDate"]),
                                Price = Convert.ToDouble(dr["price"]),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                ProductName = dr["productName"].ToString()
                            }).FirstOrDefault();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(product);
        }

        public Task<List<ProductDetails>> GetProductByProductName(string query)
        {
            List<ProductDetails> products = new List<ProductDetails>();
            try

            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());

                SqlCommand cmd = new SqlCommand("Get_Product_By_Name", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("query", query);

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                DataSet ds = new DataSet();

                da.Fill(ds);

                DataTable dt_products = ds.Tables[0];

                products = (from DataRow dr in dt_products.Rows
                            select new ProductDetails()
                            {
                                Id = Convert.ToInt32(dr["pkProductId"]),
                                Avatar = dr["avatar"].ToString(),
                                Description = dr["description"].ToString(),
                                Brand = dr["brand"].ToString(),
                                ExpiryDate = Convert.ToDateTime(dr["expiryDate"]),
                                Price = Convert.ToDouble(dr["price"]),
                                Quantity = Convert.ToInt32(dr["quantity"]),
                                ProductName = dr["productName"].ToString()
                            }).ToList();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(products);
        }
    }
}
