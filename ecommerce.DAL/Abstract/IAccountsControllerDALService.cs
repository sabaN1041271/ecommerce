using ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.DAL.Abstract
{
    public interface IAccountsControllerDALService
    {
        Task<List<UserDetails>> GetAllUsers();
        Task<UserDetails> GetUserById(int userId);
    }
}
