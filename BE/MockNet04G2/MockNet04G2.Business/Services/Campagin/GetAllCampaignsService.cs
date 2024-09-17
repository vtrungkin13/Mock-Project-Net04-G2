using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.DTOs.Users.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class GetAllCampaignsService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        public GetAllCampaignsService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignDtoMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<CampaignResponseDto>, string>> ExecuteAsync()
        {
            var response = new ApiResponse<List<CampaignResponseDto>, string>();

            var campaigns = await _campaignRepository.GetAllCampaignsAsync();

            var campaignsDto = _mapper.Map<List<CampaignResponseDto>>(campaigns);

            response.Body = campaignsDto;
            response.Status = Core.Common.Enums.StatusResponseEnum.Success;

            return response;
        }
    }
}
