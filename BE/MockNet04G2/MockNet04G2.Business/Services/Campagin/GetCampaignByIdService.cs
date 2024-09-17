using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class GetCampaignByIdService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        public GetCampaignByIdService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<CampaignByIdDto, string>> ExecuteAsync(int id)
        {
            var apiResponse = new ApiResponse<CampaignByIdDto, string>();
            var campaign = await _campaignRepository.GetCampaignByIdAsync(id);

            if (campaign == null)
            {
                apiResponse.Error = ErrorMessages.CampaginByIdNotFound;
                apiResponse.Status = Core.Common.Enums.StatusResponseEnum.NotFound;

                return apiResponse;
            }

            var campaignDtos = _mapper.Map<CampaignByIdDto>(campaign);

            apiResponse.Body = campaignDtos;
            apiResponse.Status = Core.Common.Enums.StatusResponseEnum.Success;

            return apiResponse;
        }
    }
}
