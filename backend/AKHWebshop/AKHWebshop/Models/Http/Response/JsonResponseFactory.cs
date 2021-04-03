using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Response
{
    public class JsonResponseFactory : IActionResultFactory
    {
        public ActionResult CreateResponse(int statusCode, object message)
        {
            if (message is string)
            {
                message = new {message = (string) message};
            }

            return new JsonResult(message)
            {
                ContentType = "application/json", StatusCode = statusCode
            };
        }
    }
}