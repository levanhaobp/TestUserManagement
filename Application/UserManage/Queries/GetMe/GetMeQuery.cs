using Domain.Entities;
using MediatR;

namespace Application.UserManage.Queries.GetMe
{
    public class GetMeQuery : IRequest<User>
    {
    }
}
