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
        private readonly EndDateService _endDateService;
        private readonly CheckLimitationService _checkLimitationService;
        public GetCampaignByIdService(ICampaignRepository campaignRepository, EndDateService endDateService, CheckLimitationService checkLimitationService)
        {
            _campaignRepository = campaignRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignDtoMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _endDateService = endDateService;
            _checkLimitationService = checkLimitationService;
        }

        public async Task<ApiResponse<CampaignResponseDto, string>> ExecuteAsync(int id)
        {
            var response = new ApiResponse<CampaignResponseDto, string>();
            var campaign = await _campaignRepository.GetCampaignByIdAsync(id);

            if (campaign == null)
            {
                response.Error = ErrorMessages.CampaginByIdNotFound;
                response.Status = Core.Common.Enums.StatusResponseEnum.NotFound;

                return response;
            }

            var campaignDtos = _mapper.Map<CampaignResponseDto>(campaign);

            response.Body = campaignDtos;
            response.Status = Core.Common.Enums.StatusResponseEnum.Success;

            await _endDateService.ExecuteAsync(campaign);
            await _checkLimitationService.ExecuteAsync(id);

            return response;
        }
    }
}
