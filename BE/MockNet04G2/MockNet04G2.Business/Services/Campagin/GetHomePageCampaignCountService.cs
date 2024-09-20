using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class GetHomePageCampaignCountService
    {
        private readonly ICampaignRepository _campaignRepository;
        public GetHomePageCampaignCountService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<ApiResponse<int, string>> ExecuteAsync(string code, string phone, StatusEnum? status)
        {
            var response = new ApiResponse<int, string>();
            var count = await _campaignRepository.GetHomePageCampaignCountAsync(code,phone,status);

            response.Body = count;
            response.Status = Core.Common.Enums.StatusResponseEnum.Success;

            return response;
        }
    }
}
