using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Domain.Helpers.Enums;

namespace Application.UserManage.Queries.GetListUser
{
    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<User>>
    {
        protected readonly IApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetListUserQueryHandler(IApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<User>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Chưa xác thực người dùng");
            //check role userRole
            var userRole = _httpContextAccessor.GetCurrentUserrole();
            if (string.IsNullOrEmpty(userRole) || userRole == ERole.USER.GetHashCode().ToString())
                throw new UnauthorizedAccessException("Không có quyền xem users");
            return await _applicationContext.Users.ToListAsync();
        }
    }
}
