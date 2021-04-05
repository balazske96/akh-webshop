using System;
using System.Linq;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator(ShopDataContext dataContext)
        {
            // Validate if product is present on size records
            RuleFor(product => product.Id).Must(id =>
                !dataContext.SizeRecords.Any(sizerec => sizerec.ProductId == Guid.Parse(id))
            ).WithMessage("status cannot be sold out if the amount is bigger than zero");

            // Validate for unique name
            RuleFor(product => Tuple.Create(product.Name, product.Id)).Must(args =>
                !dataContext.Products.Any(prod => prod.Name == args.Item1 && prod.Id != Guid.Parse(args.Item2))
            ).WithMessage("a product with the provided name already exists");

            // Validate for unique display name
            RuleFor(product => Tuple.Create(product.DisplayName, product.Id)).Must(args =>
                !dataContext.Products.Any(prod => prod.DisplayName == args.Item1 && prod.Id != Guid.Parse(args.Item2))
            ).WithMessage("a product with the provided display name already exists");

            // Validate for unique image name
            RuleFor(product => Tuple.Create(product.ImageName, product.Id)).Must(args =>
                !dataContext.Products.Any(prod => prod.ImageName == args.Item1 && prod.Id != Guid.Parse(args.Item2))
            ).WithMessage("a product with the provided image name already exists");

            // Validate for price
            RuleFor(product => product.Price)
                .GreaterThan((uint) 0)
                .WithMessage("product's price cannot be null");
        }
    }
}