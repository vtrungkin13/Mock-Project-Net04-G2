using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MockNet04G2.Business.DTOs.Authentication.Requests;
using MockNet04G2.Business.DTOs.Authentication.Responses;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication
{
    public class RegisterService
    {
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AuthenticationOption> _authOption;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterService(IValidator<RegisterRequest> validator, IUserRepository userRepository, 
            IOptions<AuthenticationOption> authOption, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _userRepository = userRepository;
            _authOption = authOption;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<AuthenticationDto, string>> ExecuteAsync(RegisterRequest request)
        {
            _validator.ValidateAndThrow(request);

            var response = new ApiResponse<AuthenticationDto, string>();

            var existingEmail = await _userRepository.FindUserByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                response.Error = ErrorMessages.EmailAlreadyExists;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            var existingPhone = await _userRepository.FindUserByPhoneAsync(request.Phone);
            if(existingPhone != null)
            {
                response.Error = ErrorMessages.PhonelAlreadyExists;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            if (request.Password != request.ConfirmPassword)
            {
                response.Error = ErrorMessages.ConfirmPasswordDoesNotMatch;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            var newUser = new Core.Models.User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Dob = request.Dob,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = RoleEnum.User
            };

            _userRepository.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            //create token for new user           
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_authOption.Value.Key);
            var claim = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),                
                new Claim("userId", newUser.Id.ToString()),
                new Claim(ClaimTypes.Role, "User")
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
                    Id = newUser.Id,
                    Email = newUser.Email,
                    Name = newUser.Name,
                    Phone = newUser.Phone,
                    Dob = newUser.Dob,
                    Role = newUser.Role,
                }
            };

            response.Status = StatusResponseEnum.Success;
            response.Body = authenticationDto;
            return response;
        }
    }
}
