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
    public class ChangeUserRoleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeUserRoleService(IUserRepository userRepository, IUnitOfWork unitOfWork) 
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<string,string>> ExecuteAsync(int userId, RoleEnum newRole)
        {
            var response = new ApiResponse<string, string>();
            var user = await _userRepository.FindUserByIdAsync(userId);
            user.Role = newRole;
            await _userRepository.ChangeUserRoleAsync(user);
            int saved = await _unitOfWork.SaveChangesAsync();

            if (saved <= 0) {
                response.Error = ErrorMessages.ChangeRoleFailed;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            response.Body = "Cập nhật thành công!";
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}
