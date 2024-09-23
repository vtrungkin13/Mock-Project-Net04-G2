using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class CheckLimitationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDonateRepository _donateRepository;
        private readonly ICampaignRepository _campaignRepository;
        public CheckLimitationService(IUnitOfWork unitOfWork, IDonateRepository donateRepository, ICampaignRepository campaignRepository) 
        { 
            _unitOfWork = unitOfWork;
            _donateRepository = donateRepository;
            _campaignRepository = campaignRepository;
        }
        public async Task ExecuteAsync(int id)
        {
            var campaign = await _campaignRepository.GetCampaignByIdAsync(id);
            var totalDonationAmount = await _donateRepository.GetTotalDonateAmount(id);
            if (totalDonationAmount >= campaign.Limitation && campaign.Status != Core.Common.Enums.StatusEnum.Closed) 
            { 
                campaign.Status = Core.Common.Enums.StatusEnum.Completed;
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
