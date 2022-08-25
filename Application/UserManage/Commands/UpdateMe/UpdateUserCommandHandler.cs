using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.UserManage.Commands.UpdateMe
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        protected readonly IApplicationContext _applicationContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserCommandHandler(IApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationContext = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("Chưa xác thực người dùng");

            //check user create
            var user = await _applicationContext.Users.FirstOrDefaultAsync(p => p.Id == Guid.Parse(userId));
            if (user == null)
                throw new HttpStatusException("Tài khoản không tồn tại", 400);

            user.Password = request.Password;
            user.Name = request.Name;
            user.Email = request.Email;

            _applicationContext.Users.Update(user);
            await _applicationContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
