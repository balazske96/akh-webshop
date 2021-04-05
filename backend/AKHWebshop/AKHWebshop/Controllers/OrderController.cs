#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.Abstract;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Models.Shop.Data;
using AKHWebshop.Services.DTO;
using AKHWebshop.Services.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IAkhMailClient _mailClient;
        private readonly ShopDataContext _dataContext;
        private readonly IActionResultFactory<JsonResult> _jsonResponseFactory;
        private readonly IRequestMapper _requestMapper;
        private readonly IModelMerger _modelMerger;

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
        public async Task<ActionResult> GetAllOrder([FromQuery] GetLimitedItemRequest request)
        {
            List<Order> products = await _dataContext.Orders
                .Skip(request.Skip)
                .Take(request.Limit)
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
        public async Task<ActionResult> CreateOrder(CreateOrderRequest request)
        {
            Order order = _requestMapper.CreateOrderRequestToOrder(request);

            _dataContext.Add(order);
            await _dataContext.SaveChangesAsync();
            _mailClient.SendNewOrderMail(order);

            return _jsonResponseFactory.CreateResponse(200, order);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateOrder(UpdateOrderRequest request)
        {
            Order subjectOrder = await _dataContext.Orders.FindAsync(Guid.Parse(request.Id));
            Order mappedOrder = _requestMapper.UpdateOrderRequestToOrder(request);

            _modelMerger.CopyValues(subjectOrder, mappedOrder);
            _dataContext.Update(mappedOrder);

            await _dataContext.SaveChangesAsync();
            return _jsonResponseFactory.CreateResponse(200, mappedOrder);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeletedOrder(string id)
        {
            Order subjectOrder = await _dataContext.Orders.FindAsync(Guid.Parse(id));

            if (subjectOrder == null)
            {
                return _jsonResponseFactory
                    .CreateResponse(
                        420,
                        "order with the specified id does not exist"
                    );
            }

            _dataContext.Orders.Remove(subjectOrder);
            await _dataContext.SaveChangesAsync();

            return _jsonResponseFactory.CreateResponse(200, "order deleted");
        }
    }
}