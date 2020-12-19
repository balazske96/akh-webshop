using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using AKHWebshop.Controllers;
using AKHWebshop.Models.Mail;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AKHWebshop.Test
{
    public class OrderControllerTests : ControllerTesterBase
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public OrderControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        public OrderController CreateTestController(ShopDataContext shopDataContext)
        {
            ILogger<Order> logger = new Logger<Order>(new LoggerFactory());
            SmtpClient emailClient = new SmtpClient();
            emailClient.Host = "mailer";
            emailClient.Port = 1025;
            emailClient.Credentials = new NetworkCredential("", "");
            emailClient.EnableSsl = false;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailClient.UseDefaultCredentials = false;

            AkhMailClient mailClient = new AkhMailClient(emailClient);
            return new OrderController(logger, shopDataContext, mailClient);
        }

        [Fact]
        public void CreateOrderShouldCreateOrderSuccessfully()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    Country = "Magyarország",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    PublicSpaceName = "Tarhonya",
                    Email = "nagybela@gmail.com",
                    HouseNumber = 4,
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(newOrder)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnCountryCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    HouseNumber = 4,
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "country field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnFirstNameCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    LastName = "Béla",
                    ZipCode = "4400",
                    HouseNumber = 4,
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "first name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreatOrderShouldReturnLastNameCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    ZipCode = "4400",
                    HouseNumber = 4,
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "last name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnHouseNumberCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "house number field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnTotalPriceCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    HouseNumber = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "total price field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnDoorCannotBeNullIfFloorNotNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    TotalPrice = 2800,
                    Floor = 1,
                    ZipCode = "4400",
                    HouseNumber = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new
                    {error = "door cannot be null if floor field is provided field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnCityCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    TotalPrice = 2800,
                    ZipCode = "4400",
                    HouseNumber = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new
                    {error = "city field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnZipCodeCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    Country = "Magyarország",
                    City = "Nyíregyháza",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    TotalPrice = 2800,
                    HouseNumber = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new
                    {error = "zip code field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Theory]
        [InlineData("adss")]
        [InlineData("12344")]
        [InlineData("12s1")]
        public void CreateOrderShouldReturnInvalidZipCodeFormat(string zipCode)
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    Country = "Magyarország",
                    City = "Nyíregyháza",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = zipCode,
                    TotalPrice = 2800,
                    HouseNumber = 4,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new
                    {error = "zip code is invalid"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnOrderAmountTooMuch()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    Country = "Magyarország",
                    City = "Nyíregyháza",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    TotalPrice = 2800,
                    HouseNumber = 2,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 4,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new
                {
                    error =
                        "you are trying to make an order in which the product number is more than the quantity in stock"
                })
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnProductSizeNotInStock()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL},
                        new SizeRecord() {Quantity = 0, Size = Size.L}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    Country = "Magyarország",
                    City = "Nyíregyháza",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    TotalPrice = 2800,
                    HouseNumber = 2,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.L,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new
                {
                    error = "the selected size not in the stock at the moment"
                })
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnEmailCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    HouseNumber = 2,
                    LastName = "Béla",
                    ZipCode = "4400",
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "email is not provided"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }


        [Theory]
        [InlineData("ads.com")]
        [InlineData("asd@asd.")]
        [InlineData("aasd@asd+#.com")]
        public void CreateOrderShouldReturnEmailInvalidError(string email)
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    HouseNumber = 2,
                    Email = email,
                    LastName = "Béla",
                    ZipCode = "4400",
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };

                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "email is invalid"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateOrderShouldReturnPublicSpaceNameCannotBeNullError()
        {
            using (ShopDataContext dataContext = CreateDataContext())
            {
                Product shirt = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                dataContext.Products.Add(shirt);
                dataContext.SaveChanges();

                Order newOrder = new Order()
                {
                    City = "Nyíregyháza",
                    Country = "Magyarország",
                    State = "Szabolcs-Szatmár-Bereg-megye",
                    Comment = "Légyszi csengess",
                    FirstName = "Nagy",
                    LastName = "Béla",
                    ZipCode = "4400",
                    HouseNumber = 4,
                    Email = "email@gmail.com",
                    TotalPrice = 2800,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Amount = 1,
                            Size = Size.XL,
                            ProductId = shirt.Id
                        }
                    }
                };


                OrderController controller = CreateTestController(dataContext);
                JsonResult actualResult = controller.CreateOrder(newOrder);
                JsonResult expectedResult = new JsonResult(new {error = "public space name field cannot be null"})
                {
                    ContentType = "application/json", StatusCode = 420
                };
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }
    }
}