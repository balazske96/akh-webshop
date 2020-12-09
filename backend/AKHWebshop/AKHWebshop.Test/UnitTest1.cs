using System;
using AKHWebshop.Controllers;
using AKHWebshop.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AKHWebshop.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger<Product> logger = new Logger<Product>(loggerFactory);
            ProductController controller = new ProductController(logger);
            JsonResult shouldBeTheResult = new JsonResult(new Product() {Name = "valami"});
            JsonResult actualResult = controller.GetProducts();
            Assert.Equal(((dynamic) shouldBeTheResult.Value).Name, ((dynamic) actualResult.Value).Name);
        }
    }
}