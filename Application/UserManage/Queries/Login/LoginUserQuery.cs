using MediatR;

namespace Application.UserManage.Queries.Login
{
    public class LoginUserQuery : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
