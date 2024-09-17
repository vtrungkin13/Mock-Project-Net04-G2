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
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<CampaignByIdDto>, string>> ExecuteAsync(int page,int pageSize)
        {
            var apiResponse = new ApiResponse<List<CampaignByIdDto>, string>();
            var campaigns = await _campaignRepository.CampaignPagingAsync(page,pageSize);

            var campaignsDto = _mapper.Map<List<CampaignByIdDto>>(campaigns);

            apiResponse.Body = campaignsDto;
            apiResponse.Status = StatusResponseEnum.Success;

            return apiResponse;
        }
    }
}
