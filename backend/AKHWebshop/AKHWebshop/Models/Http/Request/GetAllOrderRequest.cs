using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Models.Http.Request
{
    public class GetAllOrderRequest
    {
        [FromQuery] public int Skip { get; set; } = 0;
        [FromQuery] public int Limit { get; set; } = 10;
    }
}