using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Domain.Helpers.Enums;

namespace Application.UserManage.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
    {
        protected readonly IApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteUserCommandHandler(IApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Chưa xác thực người dùng");

            //check role userRole
            var userRole = _httpContextAccessor.GetCurrentUserrole();
            if (string.IsNullOrEmpty(userRole) || userRole == ERole.USER.GetHashCode().ToString())
                throw new UnauthorizedAccessException("Không có quyền xóa user");

            //check user delete
            var userRemove = await _applicationContext.Users.FirstOrDefaultAsync(p => p.Id == request.UserId);
            if (userRemove == null)
                throw new HttpStatusException("Tài khoản không tồn tại", 400);

            _applicationContext.Users.Remove(userRemove);
            await _applicationContext.SaveChangesAsync();

            return userRemove.Id;
        }
    }
}
