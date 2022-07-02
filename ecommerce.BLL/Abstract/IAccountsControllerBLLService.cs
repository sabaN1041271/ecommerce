using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.BLL.Abstract
{
    public interface IAccountsControllerBLLService
    {
        Task<List<UserDetails>> GetAllUsers();
        Task<UserDetails> GetUserById(int userId);
    }
}
