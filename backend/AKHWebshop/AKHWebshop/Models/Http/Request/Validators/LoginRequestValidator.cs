using System.Linq;
using AKHWebshop.Models.Auth;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Shop.Data;
using FluentValidation;

namespace AKHWebshop.Models.Http.Request.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(ShopDataContext dataContext)
        {
            RuleFor(request => request)
                .Custom((request, context) =>
                {
                    AppUser user = dataContext.Users.FirstOrDefault(u => u.UserName == request.UserName);
                    bool passwordIsInvalid = user?.Password == request.Password;
                    if (user == null || passwordIsInvalid)
                        context.AddFailure("Credentials", "username or password is incorrect");
                });
        }
    }
}