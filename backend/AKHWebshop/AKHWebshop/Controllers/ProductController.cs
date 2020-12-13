using System;
using System.Collections.Generic;
using System.Linq;
using AKHWebshop.Models.Shop;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("{id?}")]
        public JsonResult GetProducts(string? id = null, [FromQuery] int? page = null, [FromQuery] int? limit = null)
        {
            if (id != null)
            {
                Product selectedProduct = _dataContext.Products.Find(Guid.Parse(id));
                if (selectedProduct != null)
                {
                    return new JsonResult(selectedProduct)
                    {
                        ContentType = "application/json", StatusCode = 200
                    };
                }

                return new JsonResult(new {error = "product not found"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            if (limit.HasValue)
            {
                int newPageValue = page ?? 1;
                int skip = newPageValue * limit.Value;
                return new JsonResult(_dataContext.Products.Skip(skip).Take(limit.Value).ToList())
                {
                    ContentType = "application/json", StatusCode = 200
                };
            }

            JsonResult result =
                new JsonResult(_dataContext.Products.ToList())
                {
                    ContentType = "application/json", StatusCode = 200
                };
            return result;
        }

        [HttpPost]
        public JsonResult CreateProduct([FromBody] Product product)
        {
            try
            {
                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();
                return new JsonResult(product);
            }
            catch (InvalidOperationException)
            {
                return new JsonResult(new {error = "couldn't create product"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }
        }

        [HttpPut]
        public JsonResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                _dataContext.Update(product);
                _dataContext.SaveChanges();
                return new JsonResult(product);
            }
            catch (InvalidOperationException)
            {
                return new JsonResult(new {error = "couldn't create product"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }
        }
    }
}