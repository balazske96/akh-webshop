using System.Collections.Generic;
using System.Linq;
using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator(ShopDataContext shopDataContext)
        {
            // Validate for duplicated amount of size
            RuleFor(product => product.Amount).Must(amount =>
            {
                if (amount == null)
                    return true;
                HashSet<Size> sizes = new HashSet<Size>();
                List<SizeRecord> duplicates = amount.Where(sizeRec => !sizes.Add(sizeRec.Size)).ToList();
                return duplicates.Count == 0;
            }).WithMessage("product model's amount cannot contains duplicated sizes");

            // Validate for duplicated names
            RuleFor(product => product.Name).Must(name =>
                shopDataContext.Products.Any(prod => prod.Name == name)
            ).WithMessage("product with the specified name already exists");

            // Validate for duplicated image names
            RuleFor(product => product.ImageName).Must(name =>
                shopDataContext.Products.Any(prod => prod.ImageName == name)
            ).WithMessage("product with the specified image name already exists");

            // Validate for duplicated display names
            RuleFor(product => product.DisplayName).Must(name =>
                shopDataContext.Products.Any(prod => prod.DisplayName == name)
            ).WithMessage("product with the specified display name already exists");

            // Validate for price
            RuleFor(product => product.Price)
                .Must(price => price > 0)
                .WithMessage("product's price cannot be null");
        }
    }
}