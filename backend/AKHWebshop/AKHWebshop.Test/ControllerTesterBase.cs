using System;
using AKHWebshop.Controllers;
using AKHWebshop.Models.Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Test
{
    public class ControllerTesterBase
    {
        protected ShopDataContext CreateDataContext()
        {
            DbContextOptionsBuilder<ShopDataContext> builder = new DbContextOptionsBuilder<ShopDataContext>();
            builder.UseInMemoryDatabase("ShopDatabase_" + DateTime.Now.ToFileTimeUtc());
            var options = builder.Options;
            ShopDataContext newContext = new ShopDataContext(options);
            return newContext;
        }
    }
}