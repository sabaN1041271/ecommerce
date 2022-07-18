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
    public class AccountsControllerDALService : IAccountsControllerDALService
    {
        ConnectionStringManager connectionStringManager;
        SqlConnection con = null;
       
        public Task<List<UserDetails>> GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            try

            {
                DataSet ds = GetUsers();

                DataTable dt_users = ds.Tables[0];
                DataTable dt_users_addresses = ds.Tables[1];

                users = (from DataRow dr in dt_users.Rows
                         select new UserDetails()
                         {
                             Id = Convert.ToInt32(dr["pkUserId"]),
                             FirstName = dr["firstName"].ToString(),
                             LastName = dr["lastName"].ToString(),
                             MobileNumber = dr["mobileNumber"].ToString(),
                             gender = Convert.ToChar(dr["gender"].ToString().Trim()) == 'M' ? "Male" : "Female",
                             Email = dr["email"].ToString(),
                             Password = dr["password"].ToString(),
                             Addresses = GetUserAddresses(dt_users_addresses, Convert.ToInt32(dr["pkUserId"]))

                         }).ToList();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(users);
        }

        public DataSet GetUsers()
        {
            connectionStringManager = new ConnectionStringManager();
            con = new SqlConnection(connectionStringManager.GetConnectionString());

            SqlCommand cmd = new SqlCommand("Get_All_Users", con);

            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();

            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = cmd;

            DataSet ds = new DataSet();

            da.Fill(ds);
            return ds;
        }

        private List<AddressDetails> GetUserAddresses(DataTable dt_users_addresses, int userId)
        {
            var addresses = (from DataRow dr in dt_users_addresses.Rows
                             where Convert.ToInt32(dr["userId"]) == userId
                             select new AddressDetails()
                             {
                                 Id = Convert.ToInt32(dr["pkAddressId"]),
                                 Address = dr["address"].ToString(),
                                 UserId = Convert.ToInt32(dr["userId"]),
                             }).ToList();
            return addresses;
        }
        public Task<UserDetails> GetUserById(int userId)
        {
            UserDetails userDetails = new UserDetails();
            try

            {

                DataSet ds = GetUsers();

                DataTable dt_users = ds.Tables[0];
                DataTable dt_users_addresses = ds.Tables[1];

                userDetails = (from DataRow dr in dt_users.Rows
                               where Convert.ToInt32(dr["pkUserId"]) == userId
                               select new UserDetails()
                               {
                                   Id = Convert.ToInt32(dr["pkUserId"]),
                                   FirstName = dr["firstName"].ToString(),
                                   LastName = dr["lastName"].ToString(),
                                   MobileNumber = dr["mobileNumber"].ToString(),
                                   gender = Convert.ToChar(dr["gender"].ToString().Trim()) == 'M' ? "Male" : "Female",
                                   Email = dr["email"].ToString(),
                                   Password = dr["password"].ToString(),
                                   Addresses = GetUserAddresses(dt_users_addresses, Convert.ToInt32(dr["pkUserId"]))

                               }).FirstOrDefault();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return Task.FromResult(userDetails);
        }
    }
}
