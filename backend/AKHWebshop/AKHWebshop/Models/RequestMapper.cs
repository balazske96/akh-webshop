using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Shop.Data;
using AutoMapper;

namespace AKHWebshop.Models
{
    public class RequestMapper : IRequestMapper
    {
        private IMapper _mapper;

        public RequestMapper()
        {
            var configuration = new MapperConfiguration(
                config => { config.CreateMap<CreateProductRequest, Product>(); }
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
    }
}