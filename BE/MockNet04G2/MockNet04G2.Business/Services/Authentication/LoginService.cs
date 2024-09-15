using Azure.Core;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MockNet04G2.Business.DTOs.Authentication.Requests;
using MockNet04G2.Business.DTOs.Authentication.Responses;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication
{
    public class LoginService
    {
        private readonly IValidator<LoginRequest> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AuthenticationOption> _authOption;

        public LoginService(IValidator<LoginRequest> validator, IUserRepository userRepository, IOptions<AuthenticationOption> authOption)
        {
            _validator = validator;
            _userRepository = userRepository;
            _authOption = authOption;
        }

        public async Task<ApiResponse<AuthenticationDto, string>> ExecuteAsync(LoginRequest request)
        {
            _validator.ValidateAndThrow(request);

            var response = new ApiResponse<AuthenticationDto, string>();
            var user = await _userRepository.FindUserByEmailorPhone(request.EmailOrPhone.Trim());
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password.Trim(), user.Password))
            {
                response.Error = ErrorMessages.UserNameOrPasswordWrong;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_authOption.Value.Key);
            var claim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role == RoleEnum.Admin ? "Admin" : "User")
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddSeconds(_authOption.Value.LifeTimeInSecond),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var authenticationDto = new AuthenticationDto
            {
                Token = tokenString,
                ExpireAt = descriptor.Expires.Value,
                User = new UserDetailDto
                {
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    
                }
            };

            response.Status = StatusResponseEnum.Success;
            response.Body = authenticationDto;
            return response;
        }
    }
}
