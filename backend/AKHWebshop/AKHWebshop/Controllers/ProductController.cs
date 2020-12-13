#nullable enable
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
        public JsonResult GetAllProduct([FromQuery] int? page = null, [FromQuery] int? limit = null)
        {
            if (limit.HasValue)
            {
                int newPageValue = page ?? 1;
                int skip = newPageValue * limit.Value;
                return new JsonResult(_dataContext.Products.Include(product => product.Amount).Skip(skip)
                    .Take(limit.Value)
                    .ToList())
                {
                    ContentType = "application/json", StatusCode = 200
                };
            }

            JsonResult result =
                new JsonResult(_dataContext.Products.Include(product => product.Amount))
                {
                    ContentType = "application/json", StatusCode = 200
                };
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult GetProductById(string id)
        {
            Product? selectedProduct = _dataContext.Products.Include(product => product.Amount)
                .FirstOrDefault(product => product.Id == Guid.Parse(id));
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

        [HttpPost]
        [Route("create")]
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
        [Route("update/{id?}")]
        public JsonResult UpdateProduct(string? id, [FromBody] Product product)
        {
            try
            {
                _dataContext.Update(product);
                _dataContext.SaveChanges();
                return new JsonResult(product);
            }
            catch (InvalidOperationException)
            {
                return new JsonResult(new {error = "couldn't update product"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }
        }

        [HttpPut]
        [Route("update-amount/{id}")]
        public JsonResult UpdateProductAmount(string id, [FromBody] List<SizeRecord> sizeRecords)
        {
            Product? subjectProduct =
                _dataContext.Products.Include(product => product.Amount)
                    .FirstOrDefault(product => product.Id == Guid.Parse(id));
            if (subjectProduct == null)
            {
                return new JsonResult(new {error = "the product with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            subjectProduct.Amount = sizeRecords;
            _dataContext.Products.Update(subjectProduct);
            _dataContext.SaveChanges();

            return new JsonResult(subjectProduct)
            {
                ContentType = "application/json", StatusCode = 200
            };
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public JsonResult DeleteProduct(string id)
        {
            Product subject = _dataContext.Products.Find(Guid.Parse(id));
            if (subject == null)
            {
                return new JsonResult(new {error = "product with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            _dataContext.Products.Remove(subject);
            _dataContext.SaveChanges();
            return new JsonResult(new {message = "product deleted"})
            {
                ContentType = "application/json", StatusCode = 200
            };
        }
    }
}