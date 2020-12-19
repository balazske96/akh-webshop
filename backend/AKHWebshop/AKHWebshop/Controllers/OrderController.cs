#nullable enable
using System.Linq;
using System.Text.RegularExpressions;
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

        public OrderController(ILogger<Order> logger, ShopDataContext dataContext, IAkhMailClient mailClient)
        {
            _logger = logger;
            _mailClient = mailClient;
            _dataContext = dataContext;
        }

        public JsonResult CreateOrder([FromBody] Order order)
        {
            bool countryIsNull = order.Country == null;
            if (countryIsNull)
            {
                return new JsonResult(new {error = "country field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool firstNameIsNull = order.FirstName == null;
            if (firstNameIsNull)
            {
                return new JsonResult(new {error = "first name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
            }

            bool lastNameIsNull = order.LastName == null;
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

            bool cityIsNull = order.City == null;
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

            bool zipCodeIsInvalid = !Regex.IsMatch(order.ZipCode, @"^\d{4}$");
            if (zipCodeIsInvalid)
            {
                return new JsonResult(new
                    {error = "zip code is invalid"})
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

            _dataContext.Add(order);
            _dataContext.SaveChanges();
            return new JsonResult(order) {ContentType = "application/json", StatusCode = 200};
        }
    }
}