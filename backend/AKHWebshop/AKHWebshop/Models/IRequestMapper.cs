using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Shop.Data;

namespace AKHWebshop.Models
{
    public interface IRequestMapper
    {
        public Product CreateProductRequestToProduct(CreateProductRequest request);
        public Product UpdateProductRequestToProduct(UpdateProductRequest request);
    }
}