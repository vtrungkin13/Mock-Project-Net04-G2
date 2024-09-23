using AutoMapper;
using Azure;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class ChangeStatusService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChangeStatusService(ICampaignRepository campaignRepository, IUnitOfWork unitOfWork) 
        { 
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<CampaignDetailReponse, string>> ExecuteAsync(int id, ChangeCampaignStatusRequest request)
        {
            var campaign = await _campaignRepository.GetCampaignByIdAsync(id);
            var response = new ApiResponse<CampaignDetailReponse, string>();

            if (campaign.Status != StatusEnum.Completed)
            {
                response.Error = ErrorMessages.CannotUpdateCampaign;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            campaign.Status = StatusEnum.Closed;

            _campaignRepository.Update(campaign);
            await _unitOfWork.SaveChangesAsync();
            var campaignDetailResponse = _mapper.Map<CampaignDetailReponse>(campaign);
            response.Body = campaignDetailResponse;
            response.Status = StatusResponseEnum.Success;

            return response;
        }
    }
}
