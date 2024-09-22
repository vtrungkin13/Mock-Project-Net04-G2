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
        private readonly IValidator<ExtendCampaignRequest> _validator;
        private readonly IValidator<CampaignDetailRequest> _validatorDetail;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICooperateRepository _cooperateRepository;
        private readonly IMapper _mapper;

        public UpdateCampaignService(IValidator<ExtendCampaignRequest> validator, IValidator<CampaignDetailRequest> validatorDetail, ICampaignRepository campaignRepository, IUnitOfWork unitOfWork, IOrganizationRepository organizationRepository, ICooperateRepository cooperateRepository)
        {
            _validator = validator;
            _validatorDetail = validatorDetail;
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
            _organizationRepository = organizationRepository;
            _cooperateRepository = cooperateRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
            _organizationRepository = organizationRepository;
        }

        public async Task<ApiResponse<CampaignDetailReponse, string>> ExecuteAsync(int campaignId, ExtendCampaignRequest request)
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

        public async Task<ApiResponse<CampaignDetailReponse, string>> ExecuteAsync(int campaignId, CampaignDetailRequest request)
        {
            _validatorDetail.ValidateAndThrow(request);

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

            //if (campaign.Status != StatusEnum.JustCreated)
            //{
            //    response.Error = ErrorMessages.CannotUpdateCampaign;
            //    response.Status = StatusResponseEnum.InternalServerError;
            //    return response;
            //}

            var organizationIds = request.OrganizationIds.Distinct().ToList();
            var organizations = await _organizationRepository.FindByIdAsync(organizationIds);

            if (organizations.Count != organizationIds.Count)
            {
                response.Error = ErrorMessages.CannotFindOrganizationWithThisPhoneOrName;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            campaign.Title = request.Title;
            campaign.Description = request.Description;
            campaign.Content = request.Content;
            campaign.Image = request.Image;
            campaign.StartDate = request.StartDate;
            campaign.EndDate = request.EndDate;
            campaign.Limitation = request.Limitation;
            campaign.Status = StatusEnum.JustCreated;
            campaign.CreatedAt = DateTime.UtcNow;


            _campaignRepository.Update(campaign);

            var existingCooperations = await _cooperateRepository.FindByCampaignIdAsync(campaign.Id);
            _cooperateRepository.DeleteRange(existingCooperations);

            var newCooperations = organizations.Select(org => new Cooperate
            {
                CampaignId = campaign.Id,
                OrganizationId = org.Id
            }).ToList();

            _cooperateRepository.AddRange(newCooperations);

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
