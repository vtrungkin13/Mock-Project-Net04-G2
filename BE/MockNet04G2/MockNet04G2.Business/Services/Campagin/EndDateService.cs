using Microsoft.AspNetCore.Builder;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;
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

        public async Task ExecuteAsync(Core.Models.Campaign campaign)
        {
            if (campaign.Status != StatusEnum.Closed)
            {
                var currentDate = DateTime.Now.Date;
                var isUpdated = false;

                if (campaign.StartDate.Date <= currentDate && campaign.Status == StatusEnum.JustCreated)
                {
                    campaign.Status = StatusEnum.InProgress;
                    isUpdated = true;
                }

                if (campaign.EndDate.Date <= currentDate && campaign.Status != StatusEnum.Completed)
                {
                    campaign.Status = StatusEnum.Completed;
                    isUpdated = true;
                }

                if (isUpdated)
                {
                    await _unitOfWork.SaveChangesAsync();
                }
            }

        }
    }
}
