#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKHWebshop.Models;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.DTO;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Models.Shop.Data;
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
        private IRequestMapper _requestMapper;
        private IActionResultFactory<JsonResult> _jsonResponseFactory;
        private ShopDataContext _shopDataContext;

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
        public async Task<ActionResult> GetAllProduct([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            int actualLimit = limit ?? 10;
            int actualSkip = skip ?? 0;
            IEnumerable<Product> products = await _shopDataContext.Products
                .Skip(actualSkip)
                .Take(actualLimit)
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
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            Product productToSave = _requestMapper.CreateProductRequestToProduct(request);
            _shopDataContext.Products.Add(productToSave);
            await _shopDataContext.SaveChangesAsync();
            return _jsonResponseFactory.CreateResponse(200, productToSave);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct(string id, [FromBody] UpdateProductRequest request)
        {
            Product productToUpdate = _requestMapper.UpdateProductRequestToProduct(request);
            productToUpdate.Id = Guid.Parse(id);

            _shopDataContext.Products.Update(productToUpdate);
            await _shopDataContext.SaveChangesAsync();
            return _jsonResponseFactory.CreateResponse(200, productToUpdate);
        }

        [HttpPut]
        [Route("{id}/update-amount")]
        public async Task<ActionResult> UpdateProductAmount(string id, [FromBody] UpdateProductAmountRequest request)
        {
            Product? subjectProduct =
                await _shopDataContext
                    .Products
                    .Include(product => product.Amount)
                    .FirstOrDefaultAsync(product => product.Id == Guid.Parse(id)) ?? null;

            if (subjectProduct == null)
            {
                return _jsonResponseFactory.CreateResponse(404, "product not found");
            }

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