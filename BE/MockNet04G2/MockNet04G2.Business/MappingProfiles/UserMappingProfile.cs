using AutoMapper;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDetailDto>()
                .ForSourceMember(src => src.Password, opt => opt.DoNotValidate());
        }
    }
}
