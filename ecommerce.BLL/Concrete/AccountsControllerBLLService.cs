using ecommerce.BLL.Abstract;
using ecommerce.DAL.Abstract;
using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Concrete
{
    public class AccountsControllerBLLService : IAccountsControllerBLLService
    {
        private readonly IAccountsControllerDALService _accountsControllerDALService;
        public AccountsControllerBLLService(IAccountsControllerDALService accountsControllerDALService)
        {
            this._accountsControllerDALService = accountsControllerDALService;
        }
        public async Task<List<UserDetails>> GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            users = await this._accountsControllerDALService.GetAllUsers();
           
            return users;
        }

      

        public async Task<UserDetails> GetUserById(int userId)
        {

            UserDetails user = await this._accountsControllerDALService.GetUserById(userId);
            return user;
        }
    }
}
