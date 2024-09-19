using Microsoft.AspNetCore.Builder;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class EndDateService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EndDateService(IUnitOfWork unitOfWork, ICampaignRepository campaignRepository) 
        {
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CheckAndCompleteCampaignsAsync()
        {
            var campaignsToUpdate = await _campaignRepository.GetCampaignsByEndDateAndStatusAsync(DateTime.UtcNow);

            foreach (var campaign in campaignsToUpdate)
            {
                campaign.Status = StatusEnum.Completed;
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
