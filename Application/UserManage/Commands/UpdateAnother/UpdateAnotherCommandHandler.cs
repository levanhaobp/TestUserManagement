using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Domain.Helpers.Enums;

namespace Application.UserManage.Commands.UpdateAnother
{
    public class UpdateAnotherCommandHandler : IRequestHandler<UpdateAnotherCommand, Guid>
    {
        protected readonly IApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateAnotherCommandHandler(IApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> Handle(UpdateAnotherCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Chưa xác thực người dùng");

            //check role userRole
            var userRole = _httpContextAccessor.GetCurrentUserrole();
            if (string.IsNullOrEmpty(userRole) || userRole == ERole.USER.GetHashCode().ToString())
                throw new UnauthorizedAccessException("Không có quyền cập nhật user");

            //check user update
            var userUpdate = await _applicationContext.Users.FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            if (userUpdate == null)
                throw new HttpStatusException("Tài khoản không tồn tại", 400);

            userUpdate.Password = request.Password;
            userUpdate.Name = request.Name;
            userUpdate.Email = request.Email;

            _applicationContext.Users.Update(userUpdate);
            await _applicationContext.SaveChangesAsync();

            return userUpdate.Id;
        }
    }
}
