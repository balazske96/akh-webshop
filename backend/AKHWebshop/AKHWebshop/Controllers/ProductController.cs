#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AKHWebshop.Models;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<Product> _logger;
        private IRequestMapper _requestMapper;
        private IActionResultFactory _jsonResponseFactory;
        private ShopDataContext _shopDataContext;

        public ProductController(
            ILogger<Product> logger,
            ShopDataContext dataContext,
            IRequestMapper requestMapper
        )
        {
            _logger = logger;
            _shopDataContext = dataContext;
            _requestMapper = requestMapper;
            _jsonResponseFactory = new JsonResponseFactory();
        }

        [HttpGet]
        public ActionResult GetAllProduct([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            int actualLimit = limit ?? 10;
            int actualSkip = skip ?? 0;
            IEnumerable<Product> products = _shopDataContext.Products
                .Skip(actualSkip)
                .Take(actualLimit)
                .ToList();
            return _jsonResponseFactory.CreateResponse(200, products);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetProductById(string id)
        {
            var selectedProduct = _shopDataContext.Products.Find(Guid.Parse(id));
            return _jsonResponseFactory.CreateResponse(200, selectedProduct);
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    return (JsonResult) _jsonResponseFactory.CreateResponse(
                        422, modelState.Errors[0].ErrorMessage
                    );
                }
            }

            Product productToSave = _requestMapper.CreateProductRequestToProduct(request);
            _shopDataContext.Products.Add(productToSave);
            _shopDataContext.SaveChanges();
            return _jsonResponseFactory.CreateResponse(200, productToSave);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateProduct(string id, [FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    return (JsonResult) _jsonResponseFactory.CreateResponse(
                        422, modelState.Errors[0].ErrorMessage
                    );
                }
            }

            Product productToUpdate = _requestMapper.UpdateProductRequestToProduct(request);
            productToUpdate.Id = Guid.Parse(id);

            _shopDataContext.Products.Update(productToUpdate);
            _shopDataContext.SaveChanges();
            return _jsonResponseFactory.CreateResponse(200, productToUpdate);
        }

        [HttpPut]
        [Route("{id}/update-amount")]
        public ActionResult UpdateProductAmount(string id, [FromBody] UpdateProductAmountRequest request)
        {
            Product? subjectProduct =
                _shopDataContext
                    .Products
                    .Include(product => product.Amount)
                    .FirstOrDefault(product => product.Id == Guid.Parse(id));

            if (subjectProduct == null)
            {
                return _jsonResponseFactory.CreateResponse(404, "product not found");
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    return (JsonResult) _jsonResponseFactory.CreateResponse(
                        422, modelState.Errors[0].ErrorMessage
                    );
                }
            }

            subjectProduct.Amount = request.SizeRecords;

            _shopDataContext.Products.Update(subjectProduct);
            _shopDataContext.SaveChanges();

            return _jsonResponseFactory.CreateResponse(200, subjectProduct);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(string id)
        {
            Product? subject = _shopDataContext.Products.Find(Guid.Parse(id));
            if (subject == null)
            {
                return _jsonResponseFactory.CreateResponse(404, "product not found");
            }

            _shopDataContext.Remove(subject);
            _shopDataContext.SaveChanges();

            return _jsonResponseFactory.CreateResponse(200, "product deleted");
        }
    }
}