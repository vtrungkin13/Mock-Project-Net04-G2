using AutoMapper;
using MockNet04G2.Business.DTOs.Users.Responses;
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
        public GetAllCampaignsService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<Campaign>> ExecuteAsync()
        {
            var campaigns = await _campaignRepository.GetAllCampaignsAsync();
            return campaigns;
        }
    }
}
