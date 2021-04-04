using AKHWebshop.Models.Http.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;

namespace AKHWebshop.Test.Services
{
    public class JsonResponseFactoryTests
    {
        [Fact]
        public void Should_CreateResponseWithMessage_When_StatusCodeLessThan399()
        {
            // Arrange
            JsonResultFactory factory = new JsonResultFactory();
            JsonResult expected = new JsonResult(new {message = "Test_Expected_Message"})
            {
                ContentType = "application/json",
                StatusCode = 200,
            };

            // Act
            JsonResult actual = factory.CreateResponse(200, "Test_Expected_Message");

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }

        [Fact]
        public void Should_CreateResponseWithErrorMessage_When_StatusCodeGreaterThan399()
        {
            // Arrange
            JsonResultFactory factory = new JsonResultFactory();
            JsonResult expected = new JsonResult(new {error = "Test_Expected_Error_Message"})
            {
                ContentType = "application/json",
                StatusCode = 404,
            };

            // Act
            JsonResult actual = factory.CreateResponse(404, "Test_Expected_Error_Message");

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }

        [Fact]
        public void Should_CreateResponseWithObjectMessage_When_ObjectProvidedAsMessage()
        {
            // Arrange
            JsonResultFactory factory = new JsonResultFactory();
            var injectedValue = new {propertyOne = "Test_PropertyOne", propertyTwo = "Test_PropertyOne"};
            JsonResult expectedJsonResult = new JsonResult(injectedValue)
            {
                ContentType = "application/json",
                StatusCode = 200
            };

            // Act
            JsonResult actualJsonResult = factory.CreateResponse(200, injectedValue);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(injectedValue),
                JsonConvert.SerializeObject(actualJsonResult.Value));
            Assert.Equal(JsonConvert.SerializeObject(expectedJsonResult),
                JsonConvert.SerializeObject(actualJsonResult));
        }
    }
}