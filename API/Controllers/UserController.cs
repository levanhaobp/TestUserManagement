using Application.UserManage.Commands.CreateUser;
using Application.UserManage.Commands.DeleteUser;
using Application.UserManage.Commands.UpdateAnother;
using Application.UserManage.Commands.UpdateMe;
using Application.UserManage.Queries.GetListUser;
using Application.UserManage.Queries.GetMe;
using Application.UserManage.Queries.Login;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController()
        {

        }
        /// <summary>
        /// Người dùng đăng nhập
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<string> Login([FromBody] LoginUserQuery query) => await Mediator.Send(query);

        /// <summary>
        /// Admin tạo user
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<Guid> Create([FromBody] CreateUserCommand command) => await Mediator.Send(command);

        /// <summary>
        /// Người dùng cập nhật thông tin
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateMe")]
        public async Task<Guid> UpdateMe([FromBody] UpdateUserCommand command) => await Mediator.Send(command);

        /// <summary>
        /// Admin cập nhật thông tin
        /// </summary>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<Guid> Update([FromBody] UpdateAnotherCommand command) => await Mediator.Send(command);

        /// <summary>
        /// Admin xóa user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<Guid> Delete([FromBody] DeleteUserCommand command) => await Mediator.Send(command);

        /// <summary>
        /// Lấy thông tin của chính tôi
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMe")]
        public async Task<User> GetMe() => await Mediator.Send(new GetMeQuery());

        /// <summary>
        /// Lấy thông tin của toàn bộ
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetList")]
        public async Task<List<User>> GetList() => await Mediator.Send(new GetListUserQuery());
    }
}
