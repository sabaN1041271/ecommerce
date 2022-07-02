using ecommerce.BLL.Abstract;
using ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsControllerBLLService _accountsControllerBLLService;
        public AccountsController(IAccountsControllerBLLService accountsControllerBLLService)
        {
            this._accountsControllerBLLService = accountsControllerBLLService;
        }
        [HttpGet]
        [Route("users")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PageWrapper<UserDetails>), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetAllUsers([FromQuery] int page, [FromQuery] int perPage)
        {


            List<UserDetails> response = await this._accountsControllerBLLService.GetAllUsers();


            if (response == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }

        [HttpGet]
        [Route("users/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDetails), 200)]
        [ProducesResponseType(typeof(void), 404)]

        public async virtual Task<IActionResult> GetOrderById([FromRoute] int userId)
        {

            UserDetails response = await this._accountsControllerBLLService.GetUserById(userId);


            if (response == null)
            {
                return this.StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {

                return this.Ok(response);
            }

        }
    }
}
