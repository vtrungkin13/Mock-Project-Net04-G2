using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class AddCampaignService
    {
        private readonly IValidator<CampaignDetailRequest> _validator;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICooperateRepository _cooperateRepository;
        private readonly IMapper _mapper;

        public AddCampaignService(IValidator<CampaignDetailRequest> validator, ICampaignRepository campaignRepository, IUnitOfWork unitOfWork, IOrganizationRepository organizationRepository, ICooperateRepository cooperateRepository)
        {
            _validator = validator;
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

        public async Task<ApiResponse<CampaignDetailReponse,string>> ExecuteAsync(CampaignDetailRequest request)
        {
            _validator.ValidateAndThrow(request);

            var response = new ApiResponse<CampaignDetailReponse, string>();


            // Check if the lengths of the organization name and phone lists match
            if (request.OrganizationNames.Count != request.OrganizationPhones.Count)
            {
                response.Error = ErrorMessages.OrganizationNameAndPhoneListsDoesnotMatch;
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            List<Organization> selectedOrganizations = new List<Organization>();

            // Iterate through the organization names and phone numbers to find matching organizations
            for (int i = 0; i < request.OrganizationNames.Count; i++)
            {
                var name = request.OrganizationNames[i];
                var phone = request.OrganizationPhones[i];

                var organization = await _organizationRepository.FindByPhoneOrNameAsync(phone, name);

                if (organization == null)
                {
                    response.Error = ErrorMessages.CannotFindOrganizationWithThisPhoneOrName;
                    response.Status = StatusResponseEnum.BadRequest;
                    return response;
                }

                // Add additional logging or feedback about which field matched
                if (organization.Phone != phone)
                {
                    response.Error = ErrorMessages.PhoneDoesnotMatchName;
                }

                selectedOrganizations.Add(organization);
            }

            //create a new campaign
            var newCampaign = new Campaign
            {
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
                Content = request.Content,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Limitation = request.Limitation,
                Status = StatusEnum.JustCreated,
                CreatedAt = DateTime.Now
            };

            _campaignRepository.Add(newCampaign);

            int saved = await _unitOfWork.SaveChangesAsync();

            if(saved <= 0)
            {
                response.Error = ErrorMessages.CannotAddCampaign;
                response.Status = StatusResponseEnum.InternalServerError;
                return response;
            }

            // Associate each organization with the new campaign using Cooperate
            foreach (var organization in selectedOrganizations)
            {
                var cooperation = new Cooperate
                {
                    CampaignId = newCampaign.Id,
                    OrganizationId = organization.Id
                };

                _cooperateRepository.Add(cooperation);
            }

            // Save the Cooperate relationships
            await _unitOfWork.SaveChangesAsync();

            var campaignDetailResponse = _mapper.Map<CampaignDetailReponse>(newCampaign);

            response.Body = campaignDetailResponse;
            response.Status = StatusResponseEnum.Success;
            return response;

        }
    }
}
