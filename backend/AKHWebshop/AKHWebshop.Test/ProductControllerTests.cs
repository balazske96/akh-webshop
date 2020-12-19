using System;
using System.Collections.Generic;
using AKHWebshop.Controllers;
using AKHWebshop.Models.Shop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AKHWebshop.Test
{
    public class ProductControllerTests : ControllerTesterBase
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private ProductController CreateTestController(ShopDataContext shopDataContext)
        {
            ILogger<Product> logger = new Logger<Product>(new LoggerFactory());
            return new ProductController(logger, shopDataContext);
        }

        [Fact]
        public void GetProductsGivesBackSameProducts()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid1 = Guid.NewGuid();
                Guid guid2 = Guid.NewGuid();
                Guid guid3 = Guid.NewGuid();
                Guid guid4 = Guid.NewGuid();

                List<SizeRecord> sizeRecord1 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
                };
                List<SizeRecord> sizeRecord2 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid2, Size = Size.S, Quantity = 5}
                };
                List<SizeRecord> sizeRecord3 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid3, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid3, Size = Size.M, Quantity = 2},
                };
                List<SizeRecord> sizeRecord4 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid4, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid4, Size = Size.M, Quantity = 2},
                };


                List<Product> expectedTestProducts = new List<Product>
                {
                    new Product()
                    {
                        Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                        Amount = sizeRecord1
                    },
                    new Product()
                    {
                        Id = guid2, DisplayName = "Best T-Shirt 2", ImageName = "image2.jpg", Name = "akh-tshirt-2",
                        Amount = sizeRecord2
                    },
                    new Product()
                    {
                        Id = guid3, DisplayName = "Best T-Shirt 3", ImageName = "image3.jpg", Name = "akh-tshirt-3",
                        Amount = sizeRecord3
                    },
                    new Product()
                    {
                        Id = guid4, DisplayName = "Best T-Shirt 4", ImageName = "image4.jpg", Name = "akh-tshirt-4",
                        Amount = sizeRecord4
                    },
                };


                shopDataContext.AddRange(expectedTestProducts);
                shopDataContext.SaveChanges();

                ProductController productController = CreateTestController(shopDataContext);

                JsonResult expectedResult = new JsonResult(expectedTestProducts)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                JsonResult actualResult = productController.GetAllProduct();
                Assert.Equal(expectedResult.ToString(), actualResult.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void GeProductShouldReturnProductWithTheSpecifiedId()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid1 = Guid.NewGuid();

                List<SizeRecord> sizeRecord1 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
                };
                var expectedTestProducts =
                    new Product()
                    {
                        Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                        Amount = sizeRecord1
                    };


                shopDataContext.Add(expectedTestProducts);
                shopDataContext.SaveChanges();

                ProductController productController = CreateTestController(shopDataContext);

                JsonResult expectedResult = new JsonResult(expectedTestProducts);
                expectedResult.ContentType = "application/json";
                expectedResult.StatusCode = 200;
                string paramGuid = guid1.ToString();
                JsonResult actualResult = productController.GetProductById(paramGuid);

                Assert.Equal(expectedResult.ToString(), actualResult.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void GetProductsShouldReturnALimitedSetOfProducts()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid1 = Guid.NewGuid();
                Guid guid2 = Guid.NewGuid();
                Guid guid3 = Guid.NewGuid();
                Guid guid4 = Guid.NewGuid();

                List<SizeRecord> sizeRecord1 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
                };
                List<SizeRecord> sizeRecord2 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid2, Size = Size.S, Quantity = 5}
                };
                List<SizeRecord> sizeRecord3 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid3, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid3, Size = Size.M, Quantity = 2},
                };
                List<SizeRecord> sizeRecord4 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid4, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid4, Size = Size.M, Quantity = 2},
                };


                var expectedTestProducts = new List<Product>
                {
                    new Product()
                    {
                        Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                        Amount = sizeRecord1
                    },
                    new Product()
                    {
                        Id = guid2, DisplayName = "Best T-Shirt 2", ImageName = "image2.jpg", Name = "akh-tshirt-2",
                        Amount = sizeRecord2
                    },
                    new Product()
                    {
                        Id = guid3, DisplayName = "Best T-Shirt 3", ImageName = "image3.jpg", Name = "akh-tshirt-3",
                        Amount = sizeRecord3
                    },
                    new Product()
                    {
                        Id = guid4, DisplayName = "Best T-Shirt 4", ImageName = "image4.jpg", Name = "akh-tshirt-4",
                        Amount = sizeRecord4
                    },
                };


                shopDataContext.AddRange(expectedTestProducts);
                shopDataContext.SaveChanges();

                ProductController productController = CreateTestController(shopDataContext);

                JsonResult actualJsonResult1 = productController.GetAllProduct(2, 2);
                JsonResult actualJsonResult2 = productController.GetAllProduct(null, 3);
                JsonResult actualJsonResult3 = productController.GetAllProduct(3, 1);
                JsonResult actualJsonResult4 = productController.GetAllProduct(1000, 2);

                Assert.Equal(2, ((List<Product>) actualJsonResult1.Value).Count);
                Assert.Equal(3, ((List<Product>) actualJsonResult2.Value).Count);
                Assert.Single(((List<Product>) actualJsonResult3.Value));
                Assert.Empty((List<Product>) actualJsonResult4.Value);
            }
        }

        [Fact]
        public void GetProductShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                ProductController controller = CreateTestController(shopDataContext);
                JsonResult actualResult = controller.GetProductById("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
                JsonResult expectedResult = new JsonResult(new {error = "product not found"})
                    {ContentType = "application/json", StatusCode = 420};
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateProductShouldCreateANewProduct()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
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


                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);


                JsonResult expectedResult = new JsonResult(newProduct)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateProductWithoutAmountShouldCreateANewProduct()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                };

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);


                JsonResult expectedResult = new JsonResult(newProduct)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateNewProductWithSameSizeRecordReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL},
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };


                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "product model's amount cannot contains duplicated sizes"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateNewProductWithoutPriceShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL},
                    }
                };


                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "product's price cannot be null"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateNewProductWithAlreadyExistingNameShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };


                shopDataContext.Add(newProduct);
                shopDataContext.SaveChanges();

                Product anotherProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "Bestest pulóver",
                    ImageName = "crewneck-2.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(anotherProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "product with the specified name already exists"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateNewProductWithAlreadyExistingDisplayNameShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };


                shopDataContext.Add(newProduct);
                shopDataContext.SaveChanges();

                Product anotherProduct = new Product()
                {
                    Name = "pulcsi-2",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck-2.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(anotherProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "product with the specified display name already exists"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void CreateNewProductWithAlreadyExistingImageNameShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };


                shopDataContext.Add(newProduct);
                shopDataContext.SaveChanges();

                Product anotherProduct = new Product()
                {
                    Name = "pulcsi-2",
                    DisplayName = "AKH Crewneck Pulóverke",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(anotherProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "product with the specified image name already exists"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductDetailsShouldUpdateSuccessfully()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    Price = 5300,
                    ImageName = "crewneck.jpg",
                };


                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);


                JsonResult expectedResult = new JsonResult(newProduct)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);


                newProduct.Name = "pulóver";

                JsonResult newExpectedResult = new JsonResult(newProduct)
                    {ContentType = "application/json", StatusCode = 200};
                JsonResult newActualResult = productController.UpdateProduct(newProduct.Id.ToString(), newProduct);

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductAmountShouldUpdateSuccessfully()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300,
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 1, Size = Size.XL},
                        new SizeRecord() {Quantity = 3, Size = Size.L}
                    }
                };

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);


                JsonResult expectedResult = new JsonResult(newProduct)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);

                List<SizeRecord> newAmount = new List<SizeRecord>
                {
                    new SizeRecord() {Quantity = 3, Size = Size.XL},
                    new SizeRecord() {Quantity = 3, Size = Size.L}
                };

                newProduct.Amount = newAmount;

                JsonResult newExpectedResult = new JsonResult(newProduct)
                    {ContentType = "application/json", StatusCode = 200};
                JsonResult newActualResult = productController.UpdateProductAmount(
                    newProduct.Id.ToString(), newAmount
                );

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductAmountShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
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


                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);

                JsonResult expectedResult = new JsonResult(newProduct)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);

                JsonResult newExpectedResult =
                    new JsonResult(new {error = "the product with the specified id does not exist"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                List<SizeRecord> amountToAdd = new List<SizeRecord>
                {
                    new SizeRecord() {Quantity = 3, Size = Size.L},
                    new SizeRecord() {Quantity = 3, Size = Size.XL},
                    new SizeRecord() {Quantity = 3, Size = Size.XXL}
                };

                JsonResult newActualResult =
                    productController.UpdateProductAmount("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", amountToAdd);

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
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

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.CreateProduct(newProduct);

                JsonResult expectedResult = new JsonResult(newProduct)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);

                newProduct.Amount.Add(new SizeRecord() {Quantity = 2, Size = Size.S});
                newProduct.Id = Guid.Empty;

                JsonResult newExpectedResult =
                    new JsonResult(new {error = "the product with the specified id does not exist"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                JsonResult newActualResult = productController.UpdateProduct(newProduct.Id.ToString(), newProduct);

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductStatusShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid = Guid.NewGuid();
                Product testProduct = new Product()
                {
                    Id = guid,
                    Amount = new List<SizeRecord>()
                    {
                        new SizeRecord()
                        {
                            Quantity = 3,
                            Size = Size.L
                        }
                    },
                    DisplayName = "Akh Póló",
                    ImageName = "akh-polo.jpg",
                    Name = "akh-polo",
                    Status = ProductStatus.Active
                };

                shopDataContext.Products.Add(testProduct);
                shopDataContext.SaveChanges();

                ProductController controller = CreateTestController(shopDataContext);
                JsonResult expectedResult =
                    new JsonResult(new {error = "status cannot be sold out if the amount is bigger than zero"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                testProduct.Status = ProductStatus.SoldOut;
                testProduct.Amount = null;

                JsonResult actualResult = controller.UpdateProduct(testProduct.Id.ToString(), testProduct);
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductStatusShouldReturnNotFoundAnd420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                ProductController controller = CreateTestController(shopDataContext);
                JsonResult expectedResult =
                    new JsonResult(new {error = "the product with the specified id does not exist"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Product testProduct = new Product() {Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")};

                JsonResult actualResult =
                    controller.UpdateProduct("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", testProduct);
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductShouldRestrictToNotProvideAmount()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid = Guid.NewGuid();
                Product testProduct = new Product()
                {
                    Id = guid,
                    Amount = new List<SizeRecord>()
                    {
                        new SizeRecord()
                        {
                            Quantity = 3,
                            Size = Size.L
                        }
                    },
                    DisplayName = "Akh Póló",
                    ImageName = "akh-polo.jpg",
                    Name = "akh-polo",
                    Status = ProductStatus.Active
                };


                shopDataContext.Products.Add(testProduct);
                shopDataContext.SaveChanges();

                ProductController controller = CreateTestController(shopDataContext);
                JsonResult expectedResult =
                    new JsonResult(new {error = "amount shouldn't be provided on product update"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                testProduct.Status = ProductStatus.SoldOut;

                JsonResult actualResult = controller.UpdateProduct(testProduct.Id.ToString(), testProduct);
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductShouldSuccessfullyUpdate()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid = Guid.NewGuid();
                Product testProduct = new Product()
                {
                    Id = guid,
                    Amount = new List<SizeRecord>()
                    {
                        new SizeRecord()
                        {
                            Quantity = 3,
                            Size = Size.L
                        }
                    },
                    DisplayName = "Akh Póló",
                    ImageName = "akh-polo.jpg",
                    Name = "akh-polo",
                    Status = ProductStatus.Active,
                    Price = 2800
                };

                shopDataContext.Products.Add(testProduct);
                shopDataContext.SaveChanges();

                testProduct.Status = ProductStatus.Hidden;
                testProduct.Amount = null;

                ProductController controller = CreateTestController(shopDataContext);
                JsonResult expectedResult =
                    new JsonResult(testProduct)
                    {
                        ContentType = "application/json", StatusCode = 200
                    };


                JsonResult actualResult = controller.UpdateProduct(testProduct.Id.ToString(), testProduct);
                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductNameShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                Product anotherProduct = new Product()
                {
                    Name = "pulcsi2",
                    DisplayName = "Bestest pulóver",
                    ImageName = "crewneck-2.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                shopDataContext.AddRange(newProduct, anotherProduct);
                shopDataContext.SaveChanges();

                anotherProduct.Name = "pulcsi";
                anotherProduct.Amount = null;

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.UpdateProduct(anotherProduct.Id.ToString(), anotherProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "a product with the provided name already exists"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateDisplayNameShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                Product anotherProduct = new Product()
                {
                    Name = "pulcsi2",
                    DisplayName = "Bestest pulóver",
                    ImageName = "crewneck-2.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                shopDataContext.AddRange(newProduct, anotherProduct);
                shopDataContext.SaveChanges();

                anotherProduct.DisplayName = "AKH Crewneck Pulóver";
                anotherProduct.Amount = null;

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.UpdateProduct(anotherProduct.Id.ToString(), anotherProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "a product with the provided display name already exists"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateImageNameShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                Product anotherProduct = new Product()
                {
                    Name = "pulcsi2",
                    DisplayName = "Bestest pulóver",
                    ImageName = "crewneck-2.jpg",
                    Amount = new List<SizeRecord>
                    {
                        new SizeRecord() {Quantity = 3, Size = Size.XL}
                    }
                };

                shopDataContext.AddRange(newProduct, anotherProduct);
                shopDataContext.SaveChanges();

                anotherProduct.ImageName = "crewneck.jpg";
                anotherProduct.Amount = null;

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.UpdateProduct(anotherProduct.Id.ToString(), anotherProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "a product with the provided image name already exists"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void UpdateProductPriceShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Product newProduct = new Product()
                {
                    Name = "pulcsi",
                    DisplayName = "AKH Crewneck Pulóver",
                    ImageName = "crewneck.jpg",
                    Price = 5300
                };

                shopDataContext.Add(newProduct);
                shopDataContext.SaveChanges();

                newProduct.Price = 0;

                ProductController productController = CreateTestController(shopDataContext);
                JsonResult actualResult = productController.UpdateProduct(newProduct.Id.ToString(), newProduct);

                JsonResult expectedResult =
                    new JsonResult(new {error = "product's price cannot be null"})
                    {
                        ContentType = "application/json", StatusCode = 420
                    };

                Assert.Equal(expectedResult.Value.ToString(), actualResult.Value.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void DeleteProductShouldDeleteTheProduct()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                Guid guid1 = Guid.NewGuid();
                Guid guid2 = Guid.NewGuid();
                Guid guid3 = Guid.NewGuid();
                Guid guid4 = Guid.NewGuid();

                List<SizeRecord> sizeRecord1 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid1, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid1, Size = Size.M, Quantity = 2},
                };
                List<SizeRecord> sizeRecord2 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid2, Size = Size.S, Quantity = 5}
                };
                List<SizeRecord> sizeRecord3 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid3, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid3, Size = Size.M, Quantity = 2},
                };
                List<SizeRecord> sizeRecord4 = new List<SizeRecord>
                {
                    new SizeRecord() {ProductId = guid4, Size = Size.L, Quantity = 3},
                    new SizeRecord() {ProductId = guid4, Size = Size.M, Quantity = 2},
                };


                var expectedTestProducts = new List<Product>
                {
                    new Product()
                    {
                        Id = guid1, DisplayName = "Best T-Shirt 1", ImageName = "image1.jpg", Name = "akh-tshirt-1",
                        Amount = sizeRecord1
                    },
                    new Product()
                    {
                        Id = guid2, DisplayName = "Best T-Shirt 2", ImageName = "image2.jpg", Name = "akh-tshirt-2",
                        Amount = sizeRecord2
                    },
                    new Product()
                    {
                        Id = guid3, DisplayName = "Best T-Shirt 3", ImageName = "image3.jpg", Name = "akh-tshirt-3",
                        Amount = sizeRecord3
                    },
                    new Product()
                    {
                        Id = guid4, DisplayName = "Best T-Shirt 4", ImageName = "image4.jpg", Name = "akh-tshirt-4",
                        Amount = sizeRecord4
                    },
                };

                shopDataContext.AddRange(expectedTestProducts);
                shopDataContext.SaveChanges();

                ProductController productController = CreateTestController(shopDataContext);

                JsonResult expectedResult = new JsonResult(expectedTestProducts)
                {
                    ContentType = "application/json", StatusCode = 200
                };

                JsonResult actualResult = productController.GetAllProduct();
                Assert.Equal(expectedResult.ToString(), actualResult.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);


                productController.DeleteProduct(((List<Product>) expectedResult.Value)[3].Id.ToString());
                actualResult = productController.GetAllProduct();

                ((List<Product>) expectedResult.Value).RemoveAt(3);

                Assert.Equal(expectedResult.ToString(), actualResult.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }

        [Fact]
        public void DeleteProductShouldReturn420()
        {
            using (ShopDataContext shopDataContext = CreateDataContext())
            {
                JsonResult expectedResult = new JsonResult(new {error = "product with the specified id does not exist"})
                {
                    ContentType = "application/json", StatusCode = 420
                };

                ProductController productController = CreateTestController(shopDataContext);

                JsonResult actualResult = productController.DeleteProduct("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

                Assert.Equal(expectedResult.ToString(), actualResult.ToString());
                Assert.Equal(expectedResult.ContentType, actualResult.ContentType);
                Assert.Equal(expectedResult.StatusCode, actualResult.StatusCode);
            }
        }
    }
}