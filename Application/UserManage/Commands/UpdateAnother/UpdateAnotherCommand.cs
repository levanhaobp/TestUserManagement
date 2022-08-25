using MediatR;

namespace Application.UserManage.Commands.UpdateAnother
{
    public class UpdateAnotherCommand : IRequest<Guid>
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
