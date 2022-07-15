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
        Task<PageWrapper<UserDetails>> GetAllUsers(int page = 1, int perPage = int.MaxValue);
        Task<UserDetails> GetUserById(int userId);
    }
}
