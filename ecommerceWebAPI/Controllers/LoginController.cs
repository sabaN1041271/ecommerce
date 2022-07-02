using ecommerce.BLL.Abstract;
using ecommerce.Model;
using ecommerceWebAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountsControllerBLLService _accountsControllerBLLService;
        public LoginController(IAccountsControllerBLLService accountsControllerBLLService)
        {
            this._accountsControllerBLLService = accountsControllerBLLService;
        }
        [HttpPost]
        [Route("login")]
        public async virtual Task<IActionResult> Login([FromBody] LoginCredentialsRequestModel userCredentials)
        {
            bool unauthorised = false;
            string pass = PasswordEncryption.EncodePasswordToBase64(userCredentials.password);
            List<UserDetails> userList = new List<UserDetails>();
            userList = await this._accountsControllerBLLService.GetAllUsers();

            foreach (var user in userList)
            {
                if (user.Email.Equals(userCredentials.email))
                {
                    var decryptedPasssword = PasswordEncryption.DecodeFrom64(user.Password);
                    if (decryptedPasssword == userCredentials.password)
                    {
                        LoginUserDetailResponseModel loginUserDetail = new LoginUserDetailResponseModel
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Token = TokenManager.GenerateToken(userCredentials.email)
                        };
                        return this.Ok(loginUserDetail);

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid credentials");
                        return this.StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
                else
                {
                    unauthorised = true;
                }
            }
            if (unauthorised)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return this.StatusCode(StatusCodes.Status401Unauthorized);
            }

            return this.Ok();
        }
    }
}
