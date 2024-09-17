using Azure.Core;
using FluentValidation;
using MockNet04G2.Business.DTOs.Users.Requests;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.User
{
    public class ChangePasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ChangePasswordRequest> _validator;

        public ChangePasswordService(IUserRepository userRepository, IValidator<ChangePasswordRequest> validator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ApiResponse<string, string>> ExecuteAsync(ChangePasswordRequest request, int userId)
        {
            _validator.ValidateAndThrow(request);
            var response = new ApiResponse<string, string>();
            if (request.NewPassword != request.ConfirmPassword) 
            {
                response.Error = ErrorMessages.ConfirmPasswordDoesNotMatch;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }
            var user = await _userRepository.FindUserByIdAsync(userId);
            if (user == null)
            {
                response.Error = ErrorMessages.CannotGetUser;
                response.Status = StatusResponseEnum.NotFound;
                return response;
            }
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                response.Error = ErrorMessages.OldPasswordIsIncorrect;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }
            if (BCrypt.Net.BCrypt.Verify(request.NewPassword, user.Password))
            {
                response.Error = ErrorMessages.NewPasswordCanNotBeTheSameOldPassword;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _unitOfWork.SaveChangesAsync();

            response.Body = "Cập nhật thành công!";
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}
