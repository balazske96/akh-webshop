using System;
using System.Linq;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class UpdateProductAmountRequestValidator : AbstractValidator<UpdateProductAmountRequest>
    {
        public UpdateProductAmountRequestValidator(ShopDataContext dataContext)
        {
            RuleFor(request => request.SizeRecords)
                .NotNull()
                .WithMessage("sizerecord field cannot be null");

            RuleFor(request => request.Id).Custom((id, context) =>
            {
                if (!dataContext.Products.Any(product => product.Id == Guid.Parse(id)))
                    context.AddFailure("Id", "product not found");
            });
        }
    }
}