using AutoMapper;
using MockNet04G2.Business.DTOs.Authentication.Responses;
using MockNet04G2.Business.DTOs.Users.Requests;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.User
{
    public class GetAllUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<List<UserDetailDto>> ExecuteAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            var userDetailDtos = _mapper.Map<List<UserDetailDto>>(users);
            return userDetailDtos;
        }
    }
}
