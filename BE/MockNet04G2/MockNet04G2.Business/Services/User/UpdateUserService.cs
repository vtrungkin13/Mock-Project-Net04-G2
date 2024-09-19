using FluentValidation;
using MockNet04G2.Business.DTOs.Users.Requests;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.User
{
    public class UpdateUserService
    {
        private readonly IValidator<UpdateUserRequest> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserService(IValidator<UpdateUserRequest> validator, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<UserDetailDto, string>> ExecuteAsync(int userId, UpdateUserRequest request)
        {
            _validator.ValidateAndThrow(request);

            var response = new ApiResponse<UserDetailDto, string>();

            var user = await _userRepository.FindUserByIdAsync(userId);
            if (user == null)
            {
                response.Error = ErrorMessages.CannotGetUser;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                user.Name = request.Name;
            }

            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                var existingPhone = await _userRepository.FindUserByPhoneAsync(request.Phone);
                if (existingPhone != null && existingPhone.Id != user.Id)
                {
                    response.Error = ErrorMessages.PhonelAlreadyExists;
                    response.Status = StatusResponseEnum.InternalServerError;
                    return response;
                }
                user.Phone = request.Phone;
            }

            if (request.Dob.HasValue)
            {
                user.Dob = request.Dob.Value;
            }

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            var userDetailDto = new UserDetailDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Dob = user.Dob,
            };

            response.Status = StatusResponseEnum.Success;
            response.Body = userDetailDto;
            return response;
        }
    }
}
