using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;
using System;
using Xunit.Sdk;

namespace MyShop.Web.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            // ARRANGE 
            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();
            var customerRepository = new Mock<IRepository<Customer>>();

            var orderController = new OrderController(
                orderRepository.Object, 
                productRepository.Object
            );

            var createOrderModel = new CreateOrderModel 
            {
                Customer = new CustomerModel
                {
                    Name = "Filip Ekberg",
                    ShippingAddress = "Test address",
                    City = "Gothenburg",
                    PostalCode = "43317",
                    Country = "Sweden"
                },
                LineItems = new []
                {
                    new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 10 },
                    new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 2 },
                }
            };

            // ACT

            orderController.Create(createOrderModel);

            // ASSERT

            orderRepository.Verify(r => r.Add(It.IsAny<Order>()), Times.AtLeastOnce());
        }
    }
}
