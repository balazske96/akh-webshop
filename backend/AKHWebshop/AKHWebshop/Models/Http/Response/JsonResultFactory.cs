using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Response
{
    public class JsonResultFactory : IActionResultFactory<JsonResult>
    {
        public JsonResult CreateResponse(int statusCode, object message)
        {
            if (message is string)
            {
                message = statusCode > 399
                    ? (object) new {error = (string) message}
                    : (object) new {message = (string) message};
            }

            return new JsonResult(message)
            {
                ContentType = "application/json",
                StatusCode = statusCode,
            };
        }
    }
}