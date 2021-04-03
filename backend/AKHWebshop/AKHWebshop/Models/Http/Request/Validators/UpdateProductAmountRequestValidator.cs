using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class UpdateProductAmountRequestValidator : AbstractValidator<UpdateProductAmountRequest>
    {
        public UpdateProductAmountRequestValidator()
        {
            RuleFor(request => request.SizeRecords)
                .NotNull()
                .WithMessage("sizerecord field cannot be null");
        }
    }
}