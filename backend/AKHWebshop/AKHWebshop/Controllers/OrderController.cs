#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.DTO;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Models.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private ILogger<OrderController> _logger;
        private IAkhMailClient _mailClient;
        private ShopDataContext _dataContext;
        private IActionResultFactory<JsonResult> _jsonResponseFactory;
        private IRequestMapper _requestMapper;
        private IModelMerger _modelMerger;

        public OrderController(
            ILogger<OrderController> logger,
            ShopDataContext dataContext,
            IAkhMailClient mailClient,
            IActionResultFactory<JsonResult> jsonResponseFactory,
            IRequestMapper requestMapper,
            IModelMerger modelMerger
        )
        {
            _logger = logger;
            _mailClient = mailClient;
            _dataContext = dataContext;
            _jsonResponseFactory = jsonResponseFactory;
            _requestMapper = requestMapper;
            _modelMerger = modelMerger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrder([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            int actualSkip = skip ?? 0;
            int actualLimit = limit ?? 10;

            List<Order> products = await _dataContext.Orders
                .Skip(actualSkip)
                .Take(actualLimit)
                .ToListAsync();

            return _jsonResponseFactory.CreateResponse(200, products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetOrder(string id)
        {
            Order? result = await _dataContext.Orders.FindAsync(Guid.Parse(id));
            if (result == null)
                return _jsonResponseFactory.CreateResponse(420,
                    "order with the specified id does not exist");
            return _jsonResponseFactory.CreateResponse(200, result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            Order order = _requestMapper.CreateOrderRequestToOrder(request);

            _dataContext.Add(order);
            await _dataContext.SaveChangesAsync();
            _mailClient.SendNewOrderMail(order);

            return _jsonResponseFactory.CreateResponse(200, order);
        }

        [HttpPut]
        [Route("{id}")]
        public JsonResult UpdateOrder(string id, [FromBody] UpdateOrderRequest request)
        {
            Order subjectOrder = _dataContext.Orders.Find(Guid.Parse(id));
            if (subjectOrder == null)
                return _jsonResponseFactory.CreateResponse(420,
                    "order with the specified id does not exist");

            Order mappedOrder = _requestMapper.UpdateOrderRequestToOrder(request);
            _modelMerger.CopyValues(subjectOrder, mappedOrder);
            _dataContext.Update(mappedOrder);
            _dataContext.SaveChanges();

            return _jsonResponseFactory.CreateResponse(200, mappedOrder);
        }

        [HttpDelete]
        [Route("{id}")]
        public JsonResult DeletedOrder(string id)
        {
            Order subjectOrder = _dataContext.Orders.Find(Guid.Parse(id));

            if (subjectOrder == null)
            {
                return _jsonResponseFactory
                    .CreateResponse(
                        420,
                        "order with the specified id does not exist"
                    );
            }

            _dataContext.Orders.Remove(subjectOrder);
            _dataContext.SaveChanges();

            return _jsonResponseFactory.CreateResponse(200, "order deleted");
        }
    }
}