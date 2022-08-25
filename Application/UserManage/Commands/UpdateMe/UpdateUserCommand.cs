using MediatR;

namespace Application.UserManage.Commands.UpdateMe
{
    public class UpdateUserCommand : IRequest<Guid>
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
