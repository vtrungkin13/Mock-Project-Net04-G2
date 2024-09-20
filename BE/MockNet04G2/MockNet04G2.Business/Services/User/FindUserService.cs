using AutoMapper;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Business.Services.Interfaces;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;

namespace MockNet04G2.Business.Services.User
{
    public class FindUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FindUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<UserDetailDto>,string>> ExecuteAsync(string name)
        {
            var response = new ApiResponse<List<UserDetailDto>, string>();
            var user = await _userRepository.FindUserByNameAsync(name);
            var userDetailDtos = _mapper.Map<List<UserDetailDto>>(user);

            if (user == null)
            {
                response.Error = ErrorMessages.CannotGetUser;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }
            response.Body = userDetailDtos; 
            response.Status = StatusResponseEnum.Success;
            return response;
        }

        public async Task<ApiResponse<UserDetailDto, string>> ExecuteAsync(int id)
        {
            var response = new ApiResponse<UserDetailDto, string>();
            var user = await _userRepository.FindUserByIdAsync(id);

            var userDetailDtos = _mapper.Map<UserDetailDto>(user);

            if (user == null)
            {
                response.Error = ErrorMessages.CannotGetUser;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }
            response.Body = userDetailDtos;
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}
