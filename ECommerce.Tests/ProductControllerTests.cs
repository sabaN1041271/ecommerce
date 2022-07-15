using ecommerce.BLL.Concrete;
using ecommerce.DAL.Abstract;
using ecommerce.DAL.Concrete;
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
    public class ProductControllerTests
    {
        Mock<IProductsControllerDALService> productDALService = new Mock<IProductsControllerDALService>();

        ProductsControllerBLLService productsControllerBLLService;

        [SetUp]
        public void Setup()
        {

            productsControllerBLLService = new ProductsControllerBLLService(productDALService.Object);
        }
        [Test]
        public async Task GetProductList()
        {
            List<ProductDetails> products = new List<ProductDetails>();
           products.Add(new ProductDetails { Id=1, Avatar = "xxxxxxxxxxxxxxxxxxxxxxxx",Description = "Tomatoes are ripened", Brand = "ASDA", ExpiryDate = DateTime.Now.AddDays(20),Price = 2.0, ProductName = "Tomatoes", Quantity = 40 });
            products.Add(new ProductDetails { Id = 2, Avatar = "xxxxxxxxxxxxxxxxxxxxxxxx", Description = "Potatoes are ripened", Brand = "ASDA", ExpiryDate = DateTime.Now.AddDays(20), Price = 2.0, ProductName = "Potatoes", Quantity = 60 });

            productDALService.Setup<Task<List<ProductDetails>>>(x => x.AllProducts()).Returns(Task.FromResult(products));
            var productController = new ProductsController(productsControllerBLLService);
            var response = await productController.GetProductList(1, 15);
            var result = response as OkObjectResult;
            Assert.AreEqual(products.Count, ((PageWrapper<ProductDetails>)result.Value).Items.Count);
        }

        [Test]
        public async Task GetProductById()
        {
            ProductDetails product = new ProductDetails { Id = 1, Avatar = "xxxxxxxxxxxxxxxxxxxxxxxx", Description = "Tomatoes are ripened", Brand = "ASDA", ExpiryDate = DateTime.Now.AddDays(20), Price = 2.0, ProductName = "Tomatoes", Quantity = 40 };
            
            productDALService.Setup<Task<ProductDetails>>(x => x.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(product));
            var productController = new ProductsController(productsControllerBLLService);
            var response = await productController.GetProductById(1);
            var result = response as OkObjectResult;
            Assert.AreEqual(product.Id, ((ProductDetails)result.Value).Id);
        }

        [Test]
        public async Task SearchByProductName()
        {
            List<ProductDetails> products = new List<ProductDetails>();
            products.Add(new ProductDetails { Id = 1, Avatar = "xxxxxxxxxxxxxxxxxxxxxxxx", Description = "Tomatoes are ripened", Brand = "ASDA", ExpiryDate = DateTime.Now.AddDays(20), Price = 2.0, ProductName = "Tomatoes", Quantity = 40 });
            products.Add(new ProductDetails { Id = 2, Avatar = "xxxxxxxxxxxxxxxxxxxxxxxx", Description = "Potatoes are ripened", Brand = "ASDA", ExpiryDate = DateTime.Now.AddDays(20), Price = 2.0, ProductName = "Tomatoes - sweet", Quantity = 60 });

            productDALService.Setup<Task<List<ProductDetails>>>(x => x.GetProductByProductName(It.IsAny<string>())).Returns(Task.FromResult(products));
            var productController = new ProductsController(productsControllerBLLService);
            var response = await productController.SearchByProductName("Tomatoes");
            var result = response as OkObjectResult;
            Assert.AreEqual(products.Count, ((PageWrapper<ProductDetails>)result.Value).Items.Count);
        }
    }
}
