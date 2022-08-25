using Domain.Helpers;
using FluentValidation;

namespace Application.UserManage.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage(string.Format(Constants.PropertyRequired, "User name"));

            RuleFor(c => c.Password).NotEmpty().WithMessage(string.Format(Constants.PropertyRequired, "Password"));
        }
    }
}
