using AKHWebshop.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<Product> _logger;

        public ProductController(ILogger<Product> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            Product product = new Product() {Name = "valami"};
            return new JsonResult(product);
        }
    }
}