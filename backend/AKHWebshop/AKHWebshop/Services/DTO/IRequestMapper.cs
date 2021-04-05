using AKHWebshop.Models.Auth;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;

namespace AKHWebshop.Services.DTO
{
    public interface IRequestMapper
    {
        public Product CreateProductRequestToProduct(CreateProductRequest request);
        public Product UpdateProductRequestToProduct(UpdateProductRequest request);
        public AppUser RegisterRequestToAppUser(RegisterUserRequest request);
        public Order CreateOrderRequestToOrder(CreateOrderRequest request);
        public Order UpdateOrderRequestToOrder(UpdateOrderRequest request);
    }
}