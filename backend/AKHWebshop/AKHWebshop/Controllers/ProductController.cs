#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AKHWebshop.Models.Shop;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Authorization;
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
        public JsonResult GetAllProduct([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            if (limit.HasValue)
            {
                List<Product> products = _dataContext.Products.Include(product => product.Amount).Skip(skip ?? 0)
                    .Take(limit.Value)
                    .ToList();
                return new JsonResult(products)
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
        public JsonResult CreateProduct([FromBody] Product product)
        {
            bool amountContainsDuplication = product.DoesAmountContainSizeDuplication();
            if (amountContainsDuplication)
            {
                return new JsonResult(new {error = "product model's amount cannot contains duplicated sizes"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool productNameAlreadyExists = _dataContext.Products.Any(prod => prod.Name == product.Name);
            if (productNameAlreadyExists)
            {
                return new JsonResult(new {error = "product with the specified name already exists"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool imageNameAlreadyExists = _dataContext.Products.Any(prod => prod.ImageName == product.ImageName);
            if (imageNameAlreadyExists)
            {
                return new JsonResult(new {error = "product with the specified image name already exists"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool productDisplayNameAlreadyExists =
                _dataContext.Products.Any(prod => prod.DisplayName == product.DisplayName);
            if (productDisplayNameAlreadyExists)
            {
                return new JsonResult(new {error = "product with the specified display name already exists"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool productPriceIsNull = product.Price == 0;
            if (productPriceIsNull)
            {
                return new JsonResult(new {error = "product's price cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }


            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
            return new JsonResult(product)
            {
                ContentType = "application/json", StatusCode = 200
            };
        }

        [HttpPut]
        [Route("{id}")]
        public JsonResult UpdateProduct(string id, [FromBody] Product product)
        {
            bool doesProductExist = _dataContext.Products.Any(prod => prod.Id == product.Id);
            if (!doesProductExist)
                return new JsonResult(new {error = "the product with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

            bool userTriesToUpdateAmountInProductModel = product.Amount != null;
            if (userTriesToUpdateAmountInProductModel)
                return new JsonResult(new {error = "amount shouldn't be provided on product update"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

            bool isAmountNotZero = _dataContext.SizeRecords.Any(sizeRec => sizeRec.ProductId == product.Id);
            bool newStatusIsSoldOut = product.Status == ProductStatus.SoldOut;
            if (isAmountNotZero && newStatusIsSoldOut)
                return new JsonResult(new {error = "status cannot be sold out if the amount is bigger than zero"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

            bool nameIsAlreadyExists =
                _dataContext.Products.Any(prod => (prod.Name == product.Name && prod.Id != product.Id));
            if (nameIsAlreadyExists)
                return new JsonResult(new {error = "a product with the provided name already exists"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

            bool displayNameAlreadyExists = _dataContext.Products.Any(prod =>
                (prod.DisplayName == product.DisplayName && prod.Id != product.Id));
            if (displayNameAlreadyExists)
                return new JsonResult(new {error = "a product with the provided display name already exists"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

            bool imageNameAlreadyExists = _dataContext.Products.Any(prod =>
                (prod.ImageName == product.ImageName && prod.Id != product.Id));
            if (imageNameAlreadyExists)
                return new JsonResult(new {error = "a product with the provided image name already exists"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

            bool productPriceIsNull = product.Price == 0;
            if (productPriceIsNull)
            {
                return new JsonResult(new {error = "product's price cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            _dataContext.Update(product);
            _dataContext.SaveChanges();
            return new JsonResult(product)
            {
                ContentType = "application/json", StatusCode = 200
            };
        }

        [HttpPut]
        [Route("{id}/update-amount")]
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
        [Route("{id}")]
        public JsonResult DeleteProduct(string id)
        {
            Product? subject = _dataContext.Products.Find(Guid.Parse(id));
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