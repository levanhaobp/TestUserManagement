using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Domain.Helpers.Enums;

namespace Application.UserManage.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        protected readonly IApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateUserCommandHandler(IApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Chưa xác thực người dùng");
            //check role userRole
            var userRole = _httpContextAccessor.GetCurrentUserrole();
            if (string.IsNullOrEmpty(userRole) || userRole == ERole.USER.GetHashCode().ToString())
                throw new UnauthorizedAccessException("Không có quyền tạo user");
                
            //check user create
            var userExist = await _applicationContext.Users.FirstOrDefaultAsync(p => p.Username == request.Username);
            if (userExist != null)
                throw new HttpStatusException("Tài khoản đã tồn tại", 400);

            var userEntity = new User()
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                Name = request.Name,
                Email = request.Email,
                Role = (sbyte)ERole.USER
            };

            _applicationContext.Users.Add(userEntity);
            await _applicationContext.SaveChangesAsync();

            return userEntity.Id;
        }
    }
}
