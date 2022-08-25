using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UserManage.Queries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IConfiguration _configuration;
        protected readonly IApplicationContext _applicationContext;
        public LoginUserQueryHandler(IConfiguration configuration, IApplicationContext applicationContext)
        {
            _configuration = configuration;
            _applicationContext = applicationContext;
        }
        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationContext.Users.FirstOrDefaultAsync(p => p.Username == request.Username && p.Password == request.Password);
            if (user != null)
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSetting:Key").Value));

                var token = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JwtSetting:Issuer").Value,
                    audience: _configuration.GetSection("JwtSetting:Audience").Value,
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
                throw new HttpStatusException("Tài khoản hoặc mật khẩu không đúng", 400);
        }
    }
}
