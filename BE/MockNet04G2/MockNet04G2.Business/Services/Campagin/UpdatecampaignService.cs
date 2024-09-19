using AutoMapper;
using FluentValidation;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campaign
{
    public class UpdateCampaignService
    {
        private readonly IValidator<UpdateCampaignRequest> _validator;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCampaignService(IValidator<UpdateCampaignRequest> validator,
                                     ICampaignRepository campaignRepository,
                                     IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<CampaignDetailReponse, string>> ExecuteAsync(int campaignId, UpdateCampaignRequest request)
        {
            _validator.ValidateAndThrow(request);

            var response = new ApiResponse<CampaignDetailReponse, string>();

            if (campaignId <= 0)
            {
                response.Error = ErrorMessages.InvalidId;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            var campaign = await _campaignRepository.GetCampaignByIdAsync(campaignId);
            if (campaign == null)
            {
                response.Error = ErrorMessages.CampaginByIdNotFound;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }


            if (campaign.Status != StatusEnum.Completed)
            {
                response.Error = ErrorMessages.CannotUpdateCampaign;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            if (request.EndDate < campaign.EndDate)
            {
                response.Error = ErrorMessages.EndDatenMustBeGreaterOrEqual;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            if (request.Limitation < campaign.Limitation)
            {
                response.Error = ErrorMessages.LimitationMustBeGreaterOrEqual;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            campaign.EndDate = request.EndDate;
            campaign.Limitation = request.Limitation;
            campaign.Status = StatusEnum.InProgress;

            _campaignRepository.Update(campaign);

            int saved = await _unitOfWork.SaveChangesAsync();
            if (saved <= 0)
            {
                response.Error = ErrorMessages.CannotUpdateCampaign;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            var campaignDetailResponse = _mapper.Map<CampaignDetailReponse>(campaign);
            response.Body = campaignDetailResponse;
            response.Status = StatusResponseEnum.Success;

            return response;
        }
    }
}
