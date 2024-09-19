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
    public class GetHomePageCampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public GetHomePageCampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignDtoMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<CampaignResponseDto>, string>> ExecuteAsync(int pageSize, int page, string code, string phone, StatusEnum? status)
        {
            var response = new ApiResponse<List<CampaignResponseDto>, string>();
            var campaigns = await _campaignRepository.GetHomePageCampaignAsync(pageSize,page,code,phone,status);

            var campaignsDto = _mapper.Map<List<CampaignResponseDto>>(campaigns);

            response.Body = campaignsDto;
            response.Status = StatusResponseEnum.Success;

            return response;
        }
    }
}
