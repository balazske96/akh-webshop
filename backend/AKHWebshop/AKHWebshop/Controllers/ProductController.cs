using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ILogger _logger;

        public ProductController(ILogger logger)
        {
            _logger = logger;
        }
    }
}