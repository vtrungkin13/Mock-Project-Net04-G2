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
        public GetCampaignByIdService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<Campaign> ExecuteAsync(int id)
        {
            var campaign = await _campaignRepository.GetCampaignByIdAsync(id);
            return campaign;
        }
    }
}
