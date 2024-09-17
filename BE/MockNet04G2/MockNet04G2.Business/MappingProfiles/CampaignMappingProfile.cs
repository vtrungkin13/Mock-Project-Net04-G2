using AutoMapper;
using Azure;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.MappingProfiles
{
    public class CampaignMappingProfile : Profile
    {
        public CampaignMappingProfile()
        {
            // Mapping Campaign to CampaignByIdDto
            CreateMap<Campaign, CampaignByIdDto>()
                .ForMember(dest => dest.Code, opt => opt.Ignore()) // Code is computed, so we ignore mapping.
                .ReverseMap();

            // Mapping Donation to CampaignByIdDonationsDto
            CreateMap<Donate, CampaignByIdDonationsDto>()
                .ReverseMap();

            // Mapping User to CampaignByIdUserDto
            CreateMap<User, CampaignByIdUserDto>()
                .ReverseMap();

            // Mapping Cooperation to CampaignByIdCooperationsDto
            CreateMap<Cooperate, CampaignByIdCooperationsDto>()
                .ReverseMap();

            // Mapping Organization to CampaignByIdOrganizationDto
            CreateMap<Organization, CampaignByIdOrganizationDto>()
                .ReverseMap();
        }
    }
}
