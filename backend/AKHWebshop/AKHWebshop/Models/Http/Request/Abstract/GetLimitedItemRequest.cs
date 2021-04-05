using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Request.Abstract
{
    public class GetLimitedItemRequest
    {
        [FromQuery(Name = "skip")] public int Skip { get; set; } = 0;
        [FromQuery(Name = "limit")] public int Limit { get; set; } = K.DefaultQueryLimit;
    }
}