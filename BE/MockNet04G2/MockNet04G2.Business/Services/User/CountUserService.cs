using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.User
{
    public class CountUserService
    {
        private readonly IUserRepository _userRepository;
        public CountUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<int, string>> ExecuteAsync()
        {
            var response = new ApiResponse<int, string>();
            var count = await _userRepository.TotalUserCountAsync();
            if (count == 0)
            {
                response.Error = ErrorMessages.UserListEmpty;
                response.Status = StatusResponseEnum.NotFound;
                return response;
            }
            response.Body = count;
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}
