using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockNet04G2.Business.MappingProfiles;

namespace MockNet04G2.Business.Services.Campagin
{
    public class CampaignsPagingService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public CampaignsPagingService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignDtoMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<CampaignResponseDto>, string>> ExecuteAsync(int page,int pageSize)
        {
            var response = new ApiResponse<List<CampaignResponseDto>, string>();
            var campaigns = await _campaignRepository.CampaignPagingAsync(page,pageSize);

            var campaignsDto = _mapper.Map<List<CampaignResponseDto>>(campaigns);

            response.Body = campaignsDto;
            response.Status = StatusResponseEnum.Success;

            return response;
        }
    }
}
