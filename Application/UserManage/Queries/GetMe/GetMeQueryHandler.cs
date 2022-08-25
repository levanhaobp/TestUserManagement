using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.UserManage.Queries.GetMe
{
    public class GetMeQueryHandler : IRequestHandler<GetMeQuery, User>
    {
        protected readonly IApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetMeQueryHandler(IApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Chưa xác thực người dùng");
            return await _applicationContext.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));
        }
    }
}
