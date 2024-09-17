using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.MappingProfiles
{
    public class CampaignMappingProfile :Profile
    {
        public CampaignMappingProfile()
        {
            CreateMap<Campaign, CampaignDetailReponse>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
