using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class FilterCampaignsByStatusService
    {
        private readonly ICampaignRepository _campaignRepository;
        public FilterCampaignsByStatusService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<Campaign>> ExecuteAsync(StatusEnum status)
        {
            var campaigns = await _campaignRepository.FilterCampaignsByStatusAsync(status);
            return campaigns;
        }
    }
}
