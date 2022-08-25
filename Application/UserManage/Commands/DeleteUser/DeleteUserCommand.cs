using MediatR;

namespace Application.UserManage.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
    }
}
