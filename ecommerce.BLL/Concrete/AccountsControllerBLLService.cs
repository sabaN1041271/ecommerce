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
        public async Task<PageWrapper<UserDetails>> GetAllUsers(int page = 1, int perPage = int.MaxValue)
        {
            List<UserDetails> users = new List<UserDetails>();
            users = await this._accountsControllerDALService.GetAllUsers();
            var totalCount = users.Count;
            PageWrapper<UserDetails> pageList = new PageWrapper<UserDetails>
            {
                Items = users.Skip((page - 1) * perPage)
                .Take(perPage).ToList(),
                PaginationInfo = new PaginationInfo
                {
                    Count = Convert.ToInt32(totalCount),
                    Page = page,
                    PerPage = perPage
                }

            };
            return pageList;
        }

      

        public async Task<UserDetails> GetUserById(int userId)
        {

            UserDetails user = await this._accountsControllerDALService.GetUserById(userId);
            return user;
        }
    }
}
