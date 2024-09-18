using AutoMapper;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Core.Common.Messages;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class GetTotalCampaignsService
    {
        private readonly ICampaignRepository _campaignRepository;
        public GetTotalCampaignsService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<ApiResponse<int, string>> ExecuteAsync()
        {
            var response = new ApiResponse<int, string>();
            var count = await _campaignRepository.TotalCampaignsCountAsync();

            response.Body = count;
            response.Status = Core.Common.Enums.StatusResponseEnum.Success;

            return response;
        }
    }
}
