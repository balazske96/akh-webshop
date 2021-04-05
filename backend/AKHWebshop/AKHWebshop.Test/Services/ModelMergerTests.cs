using AKHWebshop.Models.Shop.Data;
using AKHWebshop.Services.DTO;
using Xunit;

namespace AKHWebshop.Test.Services
{
    public class ModelMergerTests
    {
        [Fact]
        public void Should_MergeValues_When_ThereAreTwoModelToMerge()
        {
            // Arrange
            ModelMerger merger = new ModelMerger();

            Product expected = new Product()
            {
                Name = "Test_Name_Expected",
                DisplayName = "Test_DisplayName_Expected",
                Price = 0,
                ImageName = "Test_ImageName_Expected",
                Status = ProductStatus.Active
            };
            
            Product actual = new Product()
            {
                Name = "Test_Name_Expected",
                Price = 4500,
                Status = ProductStatus.Hidden
            };

            Product actualFrom = new Product()
            {
                DisplayName = "Test_DisplayName_Expected",
                ImageName = "Test_ImageName_Expected",
                Status = ProductStatus.Active
            };

            // Act
            merger.CopyValues(actual, actualFrom);

            // Assert
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DisplayName, actual.DisplayName);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.ImageName, actual.ImageName);
        }
        
    }
}