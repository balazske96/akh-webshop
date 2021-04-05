using System.Linq;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator(ShopDataContext dataContext)
        {
            // Username validation
            RuleFor(request => request.UserName)
                .MinimumLength(K.minUsernameLength)
                .WithMessage($"username must contain at least {K.minUsernameLength} characters")
                .MaximumLength(K.maxUsernameLength)
                .WithMessage($"username must be at most {K.maxUsernameLength} characters long")
                .Must(username => dataContext.Users.Any(u => u.UserName == username))
                .WithMessage("username is already in use");

            // Password validation
            RuleFor(request => request.Password)
                .MinimumLength(K.minPasswordLength)
                .WithMessage($"password must contain at least {K.minPasswordLength} characters")
                .MaximumLength(K.maxPasswordLength)
                .WithMessage($"password must be at most {K.minPasswordLength} characters long")
                .Matches(K.passwordContainsNumberRegex)
                .WithMessage("password must contain at least one number")
                .Matches(K.passwordContainsLetterRegex)
                .WithMessage("password must contain at least one letter");
        }
    }
}