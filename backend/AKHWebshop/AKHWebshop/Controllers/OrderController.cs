#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public OrderController(ILogger<OrderController> logger, ShopDataContext dataContext, IAkhMailClient mailClient)
        {
            _logger = logger;
            _mailClient = mailClient;
            _dataContext = dataContext;
        }

        [HttpGet]
        public JsonResult GetAllOrder([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            if (limit.HasValue)
            {
                List<Order> products = _dataContext.Orders.Skip(skip ?? 0)
                    .Take(limit.Value)
                    .ToList();
                return new JsonResult(products)
                {
                    ContentType = "application/json", StatusCode = 200
                };
            }

            JsonResult result =
                new JsonResult(_dataContext.Orders.ToList())
                {
                    ContentType = "application/json", StatusCode = 200
                };
            return result;
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult GetOrder(string id)
        {
            Order? result = _dataContext.Orders.Find(Guid.Parse(id));
            if (result == null)
            {
                return new JsonResult(new {error = "order with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            return new JsonResult(result)
            {
                ContentType = "application/json", StatusCode = 200
            };
        }

        [HttpPost]
        public JsonResult CreateOrder([FromBody] Order order)
        {
            JsonResult? validatedResult = ValidateOrder(order);
            if (validatedResult != null)
            {
                return validatedResult;
            }

            _dataContext.Add(order);
            _dataContext.SaveChanges();

            _mailClient.SendNewOrderMail(order);

            return new JsonResult(order) {ContentType = "application/json", StatusCode = 200};
        }

        [HttpPut]
        [Route("{id}")]
        public JsonResult UpdateOrder(string id, [FromBody] Order order)
        {
            Order subjectOrder = _dataContext.Orders.Find(Guid.Parse(id));
            if (subjectOrder == null)
            {
                return new JsonResult(new {error = "order with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            JsonResult? validatedResult = ValidateOrder(order);
            if (validatedResult != null)
            {
                return validatedResult;
            }

            _dataContext.Update(order);
            _dataContext.SaveChanges();

            return new JsonResult(order)
            {
                ContentType = "application/json", StatusCode = 200
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public JsonResult DeletedOrder(string id)
        {
            Order subjectOrder = _dataContext.Orders.Find(Guid.Parse(id));
            if (subjectOrder == null)
            {
                return new JsonResult(new {error = "order with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            _dataContext.Orders.Remove(subjectOrder);
            _dataContext.SaveChanges();

            return new JsonResult(new {message = "order deleted"})
            {
                ContentType = "application/json", StatusCode = 200
            };
        }

        private JsonResult? ValidateOrder(Order order)
        {
            bool countryIsNull = string.IsNullOrEmpty(order.Country);
            if (countryIsNull)
            {
                return new JsonResult(new {error = "country field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool firstNameIsNull = string.IsNullOrEmpty(order.FirstName);
            if (firstNameIsNull)
            {
                return new JsonResult(new {error = "first name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool lastNameIsNull = string.IsNullOrEmpty(order.LastName);
            if (lastNameIsNull)
            {
                return new JsonResult(new {error = "last name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool houseNumberIsNull = order.HouseNumber == 0;
            if (houseNumberIsNull)
            {
                return new JsonResult(new {error = "house number field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool totalPriceIsNull = order.TotalPrice == 0;
            if (totalPriceIsNull)
            {
                return new JsonResult(new {error = "total price field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool floorAndDoorValuesAreIncorrect = (order.Floor != null && order.Door == null);
            if (floorAndDoorValuesAreIncorrect)
            {
                return new JsonResult(new
                    {error = "door cannot be null if floor field is provided field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool cityIsNull = string.IsNullOrEmpty(order.City);
            if (cityIsNull)
            {
                return new JsonResult(new
                    {error = "city field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool zipCodeIsNull = order.ZipCode == null;
            if (zipCodeIsNull)
            {
                return new JsonResult(new
                    {error = "zip code field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool zipCodeIsInvalid = !Regex.IsMatch(order.ZipCode!, @"^\d{4}$");
            if (zipCodeIsInvalid)
            {
                return new JsonResult(new
                    {error = "zip code is invalid"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool sizeNotInStock = order.OrderItems.Any(
                orderItem =>
                {
                    SizeRecord? selectedSize = _dataContext.SizeRecords
                        .Find(orderItem.ProductId, orderItem.Size);
                    if (selectedSize == null)
                    {
                        return true;
                    }

                    return (selectedSize.Quantity == 0 && orderItem.Amount >= 1);
                }
            );

            if (sizeNotInStock)
            {
                return new JsonResult(new
                    {error = "the selected size not in the stock at the moment"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool orderAmountIsInvalid = order.OrderItems.Any(
                orderItem =>
                {
                    SizeRecord? selectedSizeRecord = _dataContext.SizeRecords.Find(orderItem.ProductId, orderItem.Size);
                    if (selectedSizeRecord == null)
                        return true;
                    return (orderItem.Amount > selectedSizeRecord.Quantity && selectedSizeRecord.Quantity > 0);
                }
            );
            if (orderAmountIsInvalid)
            {
                return new JsonResult(new
                {
                    error =
                        "you are trying to make an order in which the product number is more than the quantity in stock"
                })
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }


            bool emailIsNull = order.Email == null;
            if (emailIsNull)
            {
                return new JsonResult(new {error = "email is not provided"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool emailIsInvalid = !Regex.IsMatch(order.Email!,
                @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
            if (emailIsInvalid)
            {
                return new JsonResult(new {error = "email is invalid"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool publicSpaceIsNull = string.IsNullOrEmpty(order.PublicSpaceName);
            if (publicSpaceIsNull)
            {
                return new JsonResult(new {error = "public space name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool billingInfoNotEmpty = !order.billingInfoIsEmpty();
            bool billingInfoShouldBeEmpty = order.BillingInfoSameAsOrderInfo;
            if (billingInfoNotEmpty && billingInfoShouldBeEmpty)
            {
                return new JsonResult(new {error = "billing infos should be empty if same_billing_info is true"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool orderContainsInactiveProduct = order.OrderItems.Any(orderItem =>
                {
                    Product product = _dataContext.Products.Find(orderItem.ProductId);
                    return product.Status == ProductStatus.Hidden ||
                           product.Status == ProductStatus.ComingSoon ||
                           product.Status == ProductStatus.SoldOut;
                }
            );
            if (orderContainsInactiveProduct)
            {
                return new JsonResult(new {error = "order cannot contains non activated products"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            return null;
        }
    }
}