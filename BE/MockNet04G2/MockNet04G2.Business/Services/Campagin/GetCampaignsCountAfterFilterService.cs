using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Campagin
{
    public class GetCampaignsCountAfterFilterService
    {
        private readonly ICampaignRepository _campaignRepository;
        public GetCampaignsCountAfterFilterService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<ApiResponse<int, string>> ExecuteAsync(StatusEnum status)
        {
            var response = new ApiResponse<int, string>();
            var count = await _campaignRepository.TotalCampaignsCountAfterFilterAsync(status);

            response.Body = count;
            response.Status = Core.Common.Enums.StatusResponseEnum.Success;

            return response;
        }
    }
}
