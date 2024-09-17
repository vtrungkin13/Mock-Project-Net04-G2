using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.DTOs.Cooperations.Responses;
using MockNet04G2.Business.DTOs.Donate.Responses;
using MockNet04G2.Business.DTOs.Organizations.Responses;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.MappingProfiles
{
    public class CampaignDtoMappingProfile : Profile
    {
        public CampaignDtoMappingProfile()
        {
            // Mapping Campaign to CampaignByIdDto
            CreateMap<Campaign, CampaignResponseDto>()
                .ForMember(dest => dest.Code, opt => opt.Ignore()) // Code is computed, so we ignore mapping.
                .ReverseMap();

            // Mapping Donation to CampaignByIdDonationsDto
            CreateMap<Donate, DonateResponseDto>()
                .ReverseMap();

            // Mapping User to CampaignByIdUserDto
            CreateMap<User, UserDetailDto>()
                .ReverseMap();

            // Mapping Cooperation to CampaignByIdCooperationsDto
            CreateMap<Cooperate, CooperateResponseDto>()
                .ForMember(dest => dest.CampaignId, opt => opt.Ignore())
                .ForMember(dest => dest.Campaign, opt => opt.Ignore())
                .ReverseMap();

            // Mapping Organization to CampaignByIdOrganizationDto
            CreateMap<Organization, OrganizationResponseDto>()
                .ForMember(dest => dest.Cooperations, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
