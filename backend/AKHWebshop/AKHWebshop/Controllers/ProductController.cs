#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKHWebshop.Models;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.Abstract;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Models.Shop.Data;
using AKHWebshop.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<Product> _logger;
        private readonly IRequestMapper _requestMapper;
        private readonly IActionResultFactory<JsonResult> _jsonResponseFactory;
        private readonly ShopDataContext _shopDataContext;

        public ProductController(
            ILogger<Product> logger,
            ShopDataContext dataContext,
            IRequestMapper requestMapper,
            IActionResultFactory<JsonResult> jsonResponseFactory
        )
        {
            _logger = logger;
            _shopDataContext = dataContext;
            _requestMapper = requestMapper;
            _jsonResponseFactory = jsonResponseFactory;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProduct([FromQuery] GetLimitedItemRequest request)
        {
            IEnumerable<Product> products = await _shopDataContext.Products
                .Skip(request.Skip)
                .Take(request.Limit)
                .ToListAsync();
            return _jsonResponseFactory.CreateResponse(200, products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetProductById(string id)
        {
            var selectedProduct = await _shopDataContext.Products.FindAsync(Guid.Parse(id));
            return _jsonResponseFactory.CreateResponse(200, selectedProduct);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductRequest request)
        {
            Product productToSave = _requestMapper.CreateProductRequestToProduct(request);
            _shopDataContext.Products.Add(productToSave);
            await _shopDataContext.SaveChangesAsync();
            return _jsonResponseFactory.CreateResponse(200, productToSave);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct(UpdateProductRequest request)
        {
            Product productToUpdate = _requestMapper.UpdateProductRequestToProduct(request);
            productToUpdate.Id = Guid.Parse(request.Id);

            _shopDataContext.Products.Update(productToUpdate);
            await _shopDataContext.SaveChangesAsync();
            return _jsonResponseFactory.CreateResponse(200, productToUpdate);
        }

        [HttpPut]
        [Route("{id}/update-amount")]
        public async Task<ActionResult> UpdateProductAmount(UpdateProductAmountRequest request)
        {
            Product subjectProduct =
                await _shopDataContext
                    .Products
                    .Include(product => product.Amount)
                    .FirstOrDefaultAsync(product => product.Id == Guid.Parse(request.Id));

            subjectProduct.Amount = request.SizeRecords;

            _shopDataContext.Products.Update(subjectProduct);
            await _shopDataContext.SaveChangesAsync();

            return _jsonResponseFactory.CreateResponse(200, subjectProduct);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            Product? subject = await _shopDataContext.Products.FindAsync(Guid.Parse(id));
            if (subject == null)
            {
                return _jsonResponseFactory.CreateResponse(404, "product not found");
            }

            _shopDataContext.Remove(subject);
            await _shopDataContext.SaveChangesAsync();

            return _jsonResponseFactory.CreateResponse(200, "product deleted");
        }
    }
}