using ecommerce.BLL.Abstract;
using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Concrete
{
    public class AccountsControllerBLLService : IAccountsControllerBLLService
    {
        public Task<List<UserDetails>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserDetails> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
