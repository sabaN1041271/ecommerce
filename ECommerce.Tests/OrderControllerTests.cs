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
    public class OrderControllerTests
    {
        Mock<IOrderControllerDALService> orderDALService = new Mock<IOrderControllerDALService>();

        OrderControllerBLLService orderControllerBLLService;

        [SetUp]
        public void Setup()
        {
            orderControllerBLLService = new OrderControllerBLLService(orderDALService.Object);
        }

        [Test]
        public async Task GetOrderList()
        {
            List<OrderProductDetails> orders = new List<OrderProductDetails>();
            orders.Add(new OrderProductDetails
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderId = 1,
                ProductDetails = new List<ProductsAdded> { new ProductsAdded { ProductId = 1, ProductName = "Tomatoes", PriceOfSingleProduct = 4.6, ProductQuantity = 2, TotalPriceOfProduct = 4.6 * 2 } }
            });

            orderDALService.Setup<Task<List<OrderProductDetails>>>(x => x.AllOrders(It.IsAny<int>())).Returns(Task.FromResult(orders));
            var orderController = new OrderController(orderControllerBLLService);
            var ordersDone = await orderController.GetOrderList(1, 15, 1);
            var result = ordersDone as OkObjectResult;
            Assert.AreEqual(1, ((PageWrapper<OrderProductDetails>)result.Value).Items.FirstOrDefault().OrderId);
        }
        [Test]
        public async Task GetOrderById()
        {
            List<OrderProductDetails> orders = new List<OrderProductDetails>();
            orders.Add(new OrderProductDetails
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderId = 1,
                ProductDetails = new List<ProductsAdded> { new ProductsAdded { ProductId = 1, ProductName = "Tomatoes", PriceOfSingleProduct = 4.6, ProductQuantity = 2, TotalPriceOfProduct = 4.6 * 2 } }
            });

            orderDALService.Setup<Task<OrderProductDetails>>(x => x.GetOrderById(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(orders.FirstOrDefault()));
            var orderController = new OrderController(orderControllerBLLService);
            var ordersDone = await orderController.GetOrderById(1,1);
            var result = ordersDone as OkObjectResult;
            Assert.AreEqual(1, ((OrderProductDetails)result.Value).OrderId);
        }

        [Test]
        public async Task SearchByOrderDate()
        {
            List<OrderProductDetails> orders = new List<OrderProductDetails>();
            orders.Add(new OrderProductDetails
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderId = 1,
                ProductDetails = new List<ProductsAdded> { new ProductsAdded { ProductId = 1, ProductName = "Tomatoes", PriceOfSingleProduct = 4.6, ProductQuantity = 2, TotalPriceOfProduct = 4.6 * 2 } }
            });
            orders.Add(new OrderProductDetails
            {
                Id = 2,
                OrderDate = DateTime.Now.AddDays(-2),
                OrderId = 2,
                ProductDetails = new List<ProductsAdded> { new ProductsAdded { ProductId = 1, ProductName = "Tomatoes", PriceOfSingleProduct = 4.6, ProductQuantity = 2, TotalPriceOfProduct = 4.6 * 2 } }
            });

            orderDALService.Setup<Task<List<OrderProductDetails>>>(x => x.GetOrdersByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(Task.FromResult(orders));
            var orderController = new OrderController(orderControllerBLLService);
            var ordersDone = await orderController.SearchByOrderDate(DateTime.Now.AddDays(-1),DateTime.Now,1,1,15);
            var result = ordersDone as OkObjectResult;
            Assert.AreEqual(1, ((PageWrapper<OrderProductDetails>)result.Value).Items.FirstOrDefault().OrderId);
        }
        [Test]
        public async Task AddItemsToBasket()
        {
            bool added = true;

            OrderRequestModel model = new OrderRequestModel();
            model.itemDetails = new List<ItemsToAddRequestModel> { new ItemsToAddRequestModel { ProductId = 1, Quantity = 2 } };
            orderDALService.Setup<Task<bool>>(x => x.AddItemsToBasket(It.IsAny<OrderRequestModel>(), It.IsAny<int>())).Returns(Task.FromResult(added));
            var orderController = new OrderController(orderControllerBLLService);
            var response = await orderController.AddItemsToBasket(model,1);
            var result = response as OkObjectResult;
            Assert.AreEqual(added,(bool)result.Value);
        }
        [Test]
        public async Task GetItemsAddedToBasket()
        {
            BasketDetails basketDetails = new BasketDetails();
            basketDetails.BasketId = 1;
            basketDetails.UserId = 1;
            basketDetails.ProductsAddeds = new List<ProductsAdded> { new ProductsAdded { ProductId = 1, ProductName = "Tomatoes", PriceOfSingleProduct = 4.6, ProductQuantity = 2, TotalPriceOfProduct = 4.6 * 2 }, new ProductsAdded { ProductId = 2, ProductName = "Onions", PriceOfSingleProduct = 6, ProductQuantity = 2, TotalPriceOfProduct = 6 * 2 } };

            OrderRequestModel model = new OrderRequestModel();
            model.itemDetails = new List<ItemsToAddRequestModel> { new ItemsToAddRequestModel { ProductId = 1, Quantity = 2 } };
            orderDALService.Setup<Task<BasketDetails>>(x => x.GetItemsAddedToBasket(It.IsAny<int>())).Returns(Task.FromResult(basketDetails));
            var orderController = new OrderController(orderControllerBLLService);
            var response = await orderController.GetItemsAddedToBasket(1);
            var result = response as OkObjectResult;
            Assert.AreEqual(basketDetails.ProductsAddeds.Count, ((BasketDetails)result.Value).ProductsAddeds.Count);
        }
        [Test]
        public async Task UpdateItemsInBasket()
        {
            bool added = true;

            ItemsToAddRequestModel model = new ItemsToAddRequestModel { ProductId = 3, Quantity = 2 };

            orderDALService.Setup<Task<bool>>(x => x.UpdateItemsInBasket(It.IsAny<ItemsToAddRequestModel>(),It.IsAny<int>())).Returns(Task.FromResult(added));
            var orderController = new OrderController(orderControllerBLLService);
            var response = await orderController.UpdateItemsInBasket(model,1);
            var result = response as OkObjectResult;
            Assert.AreEqual(added, (bool)result.Value);
        }

        [Test]
        public async Task AddCreditCardToAccount()
        {
            bool added = true;

            CreditCardRequestModel creditCardRequestModel = new CreditCardRequestModel { CardNumber = "1234567890", CardType = "visa", Cvc = 123, ExpiryMonth = 12, ExpiryYear = 2024 };

            orderDALService.Setup<Task<bool>>(x => x.AddCreditCardToAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(added));
            var orderController = new OrderController(orderControllerBLLService);
            var response = await orderController.AddCreditCardToAccount(creditCardRequestModel, 1);
            var result = response as OkObjectResult;
            Assert.AreEqual(added, (bool)result.Value);
        }

        [Test]
        public async Task GetAllSavedCreditCards()
        {
         
            List<CreditCard> creditCards = new List<CreditCard> { new CreditCard { CardNumber = "1234567890", CardType = "visa", ExpiryMonth = 12, ExpiryYear = 2024 }, new CreditCard { CardNumber = "0987654321", CardType = "american express", ExpiryMonth = 2, ExpiryYear = 2029 } };

            orderDALService.Setup<Task<List<CreditCard>>>(x => x.GetAllSavedCreditCards(It.IsAny<int>())).Returns(Task.FromResult(creditCards));
            var orderController = new OrderController(orderControllerBLLService);
            var response = await orderController.GetAllSavedCreditCards(1,15,1);
            var result = response as OkObjectResult;
            Assert.AreEqual(creditCards.Count, ((PageWrapper<CreditCard>)result.Value).Items.Count);
        }
        [Test]
        public async Task PlaceOrder()
        {
            bool added = true;
            OrderRequestModel orderRequestModel = new OrderRequestModel {itemDetails = new List<ItemsToAddRequestModel> { new ItemsToAddRequestModel {ProductId = 1, Quantity=2 }, new ItemsToAddRequestModel { ProductId = 2, Quantity = 5 } } };
            

            orderDALService.Setup<Task<bool>>(x => x.PlaceOrder(It.IsAny<OrderRequestModel>(),It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(added));
            var orderController = new OrderController(orderControllerBLLService);
            var response = await orderController.PlaceOrder(orderRequestModel,1,1);
            var result = response as OkObjectResult;
            Assert.AreEqual(added, (bool)result.Value);
        }

    }
}
