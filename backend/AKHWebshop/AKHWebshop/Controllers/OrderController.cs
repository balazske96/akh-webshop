using AKHWebshop.Models.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase

    {
        private ILogger<Order> _logger;
        private IAkhMailClient _mailClient;
        private ShopDataContext _dataContext;

        public OrderController(ILogger<Order> logger, IAkhMailClient mailClient, ShopDataContext dataContext)
        {
            _logger = logger;
            _mailClient = mailClient;
            _dataContext = dataContext;
        }
    }
}