using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MockNet04G2.Business.MappingProfiles;
using MockNet04G2.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using MockNet04G2.Core.Models;

namespace MockNet04G2.Business.Services.Campagin
{
    public class SearchCampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        private readonly IOrganizationRepository _organizationRepository;
        public SearchCampaignService(ICampaignRepository campaignRepository,IOrganizationRepository organizationRepository) 
        {
            _campaignRepository = campaignRepository;
            _organizationRepository = organizationRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CampaignMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ApiResponse<List<CampaignDetailReponse>, string>> ExecuteAsync(string campaignCode, string organizationPhone)
        {
            var response = new ApiResponse<List<CampaignDetailReponse>, string>();

            if (string.IsNullOrEmpty(campaignCode) && string.IsNullOrEmpty(organizationPhone))
            {
                response.Error = "Please provide either a campaign code or organization phone number for searching.";
                response.Status = StatusResponseEnum.BadRequest;
                return response;
            }

            // Start with all campaigns
            IQueryable<Core.Models.Campaign> query = _campaignRepository.GetAll();

            // Filter by campaign code if provided
            if (!string.IsNullOrEmpty(campaignCode))
            {
                query = query.Where(c => c.Code == campaignCode);
            }

            // If organization phone is provided, join with organizations and filter by phone number
            if (!string.IsNullOrEmpty(organizationPhone))
            {
                query = from campaign in query
                        join cooperation in _campaignRepository.GetCooperations() on campaign.Id equals cooperation.CampaignId
                        join organization in _organizationRepository.GetAll() on cooperation.OrganizationId equals organization.Id
                        where organization.Phone == organizationPhone
                        select campaign;
            }

            // Execute the query and map the results to DTOs
            var campaigns = await query.ToListAsync();

            if (!campaigns.Any())
            {
                response.Error = "No campaigns found matching the given criteria.";
                response.Status = StatusResponseEnum.NotFound;
                return response;
            }

            var campaignDetails = campaigns.Select(c => _mapper.Map<CampaignDetailReponse>(c)).ToList();

            response.Body = campaignDetails;
            response.Status = StatusResponseEnum.Success;
            return response;
        }
    }
}
