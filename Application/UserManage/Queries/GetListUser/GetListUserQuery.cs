using Domain.Entities;
using MediatR;

namespace Application.UserManage.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<List<User>>
    {
    }
}
