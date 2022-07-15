using ecommerce.BLL.Abstract;
using ecommerce.BLL.Concrete;
using ecommerce.DAL.Abstract;
using ecommerce.Model;
using ecommerceWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tests
{
    [TestFixture]
    public class AccountsControllerTests
    {

        Mock<IAccountsControllerDALService> accountsDALService = new Mock<IAccountsControllerDALService>();

        AccountsControllerBLLService accountsControllerBLLService;

        [SetUp]
        public void Setup()
        {
            accountsControllerBLLService = new AccountsControllerBLLService(accountsDALService.Object);
        }

        [Test]
        public async Task GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            users.Add(new UserDetails {
                Addresses = new List<AddressDetails> { new AddressDetails { Id =1, Address = "56 Broad Street, Brampton, UK, BR6 7DK", UserId = 1} },
                Id = 1,
                FirstName = "Alice",
                LastName = "Brown",
                Email ="xxx@gmail.com",
                gender = "Male",
                MobileNumber = "+991234567890",
                Password = "Abcd@1234"
            });

            accountsDALService.Setup<Task<List<UserDetails>>>(x => x.GetAllUsers()).Returns(Task.FromResult(users));
            var accountsController = new AccountsController(accountsControllerBLLService);
            var userList = await accountsController.GetAllUsers(1, 15);
            var result = userList as OkObjectResult;
            Assert.AreEqual(1, ((PageWrapper<UserDetails>)result.Value).Items.FirstOrDefault().Id);
        }


        [Test]
        public async Task GetUserById()
        {
            List<UserDetails> users = new List<UserDetails>();
            users.Add(new UserDetails
            {
                Addresses = new List<AddressDetails> { new AddressDetails { Id = 1, Address = "56 Broad Street, Brampton, UK, BR6 7DK", UserId = 1 } },
                Id = 1,
                FirstName = "Alice",
                LastName = "Brown",
                Email = "xxx@gmail.com",
                gender = "Male",
                MobileNumber = "+991234567890",
                Password = "Abcd@1234"
            });

            accountsDALService.Setup<Task<UserDetails>>(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(users.FirstOrDefault()));
            var accountsController = new AccountsController(accountsControllerBLLService);
            var userList = await accountsController.GetUserById(1);
            var result = userList as OkObjectResult;
            Assert.AreEqual(1, ((UserDetails)result.Value).Id);
        }
    }
}
