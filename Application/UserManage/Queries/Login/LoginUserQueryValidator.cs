using Domain.Helpers;
using FluentValidation;

namespace Application.UserManage.Queries.Login
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage(string.Format(Constants.PropertyRequired, "Username"));

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage(string.Format(Constants.PropertyRequired, "Password"));
        }
    }
}
