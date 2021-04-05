using AKHWebshop.Models.Auth;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;
using AutoMapper;

namespace AKHWebshop.Services.DTO
{
    public class RequestMapper : IRequestMapper
    {
        private IMapper _mapper;

        public RequestMapper()
        {
            var configuration = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<CreateProductRequest, Product>();
                    config.CreateMap<UpdateProductRequest, Product>();
                    config.CreateMap<RegisterUserRequest, AppUser>();
                    config.CreateMap<CreateOrderRequest, Order>();
                    config.CreateMap<UpdateOrderRequest, Order>();
                }
            );

            //configuration.AssertConfigurationIsValid();
            _mapper = configuration.CreateMapper();
        }

        public Product CreateProductRequestToProduct(CreateProductRequest request)
        {
            return _mapper.Map<Product>(request);
        }

        public Product UpdateProductRequestToProduct(UpdateProductRequest request)
        {
            return _mapper.Map<Product>(request);
        }

        public AppUser RegisterRequestToAppUser(RegisterUserRequest request)
        {
            return _mapper.Map<AppUser>(request);
        }

        public Order CreateOrderRequestToOrder(CreateOrderRequest request)
        {
            return _mapper.Map<Order>(request);
        }

        public Order UpdateOrderRequestToOrder(UpdateOrderRequest request)
        {
            return _mapper.Map<Order>(request);
        }
    }
}