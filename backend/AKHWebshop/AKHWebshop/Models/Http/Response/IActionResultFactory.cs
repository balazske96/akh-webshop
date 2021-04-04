using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Response
{
    public interface IActionResultFactory<T> where T : ActionResult
    {
        public T CreateResponse(int statusCode, object message);
    }
}