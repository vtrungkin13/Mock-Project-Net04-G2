using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.DTOs.Donations.Responses;
using MockNet04G2.Core.Models;

namespace MockNet04G2.Business.MappingProfiles
{
    public class DonateHistoryMappingProfile : Profile
    {
        public DonateHistoryMappingProfile()
        {
            CreateMap<Donate, DonateHistoryResponseDto>().ReverseMap();
            CreateMap<Campaign, CampaignResponseDto>().ReverseMap();
        }
    }
}
