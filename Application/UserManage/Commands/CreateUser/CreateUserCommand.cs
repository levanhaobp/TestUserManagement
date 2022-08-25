using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.UserManage.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
