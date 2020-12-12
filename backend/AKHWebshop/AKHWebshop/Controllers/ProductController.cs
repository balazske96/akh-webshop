using System;
using System.Collections.Generic;
using System.Linq;
using AKHWebshop.Models.Shop;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<Product> _logger;
        private ShopDataContext _dataContext;

        public ProductController(ILogger<Product> logger, ShopDataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            JsonResult result = new JsonResult(_dataContext.Products.ToList());
            result.ContentType = "application/json";
            result.StatusCode = 200;
            return result;
        }

        [Route("{id}")]
        [HttpGet]
        public JsonResult GetProductsById(string id)
        {
            throw new NotImplementedException();
        }
    }
}