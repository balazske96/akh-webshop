using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AKHWebshop.Models.Http.Response
{
    public interface IActionResultFactory
    {
        public ActionResult CreateResponse(int statusCode, object message);
    }
}