using System;
using System.Linq;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator(ShopDataContext dataContext)
        {
            // HouseNumber validation 
            RuleFor(request => request.HouseNumber)
                .GreaterThan((ushort) 0)
                .WithMessage("house number has to be greater than zero");

            // Floor
            RuleFor(request => request.Floor)
                .GreaterThan((byte) 0)
                .WithMessage("floor number has to be greater than zero");

            // Door
            RuleFor(request => request.Door)
                .GreaterThan((ushort) 0)
                .WithMessage("door number has to be greater than zero");

            // Billing info
            RuleFor(request => request.BillingInfo)
                .NotNull()
                .WithMessage("Billing info cannot be empty")
                .Custom(((info, context) =>
                {
                    string CannotBeNull(string field) => $"Wrong billing info: {field} cannot be null";

                    RuleFor(info => info.City)
                        .NotNull()
                        .WithMessage(CannotBeNull("city"));

                    RuleFor(info => info.Country)
                        .NotNull()
                        .WithMessage(CannotBeNull("country"));

                    RuleFor(info => info.PublicSpaceType)
                        .NotNull()
                        .WithMessage(CannotBeNull("public space type"));

                    RuleFor(info => info.PublicSpaceName)
                        .NotNull()
                        .WithMessage(CannotBeNull("public space name"));

                    RuleFor(info => info.FirstName)
                        .NotNull()
                        .WithMessage(CannotBeNull("first name"));

                    RuleFor(info => info.LastName)
                        .NotNull()
                        .WithMessage(CannotBeNull("last name"));

                    RuleFor(info => info.State)
                        .NotNull()
                        .WithMessage(CannotBeNull("state"));

                    RuleFor(info => info.HouseNumber)
                        .NotNull()
                        .WithMessage(CannotBeNull("house number"));

                    RuleFor(info => info.Floor)
                        .NotNull()
                        .WithMessage(CannotBeNull("floor"));

                    RuleFor(info => info.Door)
                        .NotNull()
                        .WithMessage(CannotBeNull("door"));
                }));


            // Price and OrderItems
            RuleFor(request => request)
                .Custom((request, context) =>
                {
                    int computedPrice = 0;
                    foreach (OrderItem item in request.OrderItems)
                    {
                        // Check to see if the product is out of stock
                        int productAmountInDb = dataContext.Products
                            .Find(item.ProductId).Amount
                            .Where(amount => amount.Size == item.Size)
                            .ToList()
                            .Count;
                        if (productAmountInDb < item.Amount)
                            context.AddFailure("Amount", "Selected item is out of stock");

                        computedPrice += (int) item.Product.Price;
                    }

                    if (computedPrice != request.TotalPrice)
                        context.AddFailure("Price", "Prices does not match");
                });
        }
    }
}