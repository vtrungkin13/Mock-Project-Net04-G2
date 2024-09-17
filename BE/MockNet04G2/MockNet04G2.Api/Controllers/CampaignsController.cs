using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.Services.Campagin;
using MockNet04G2.Business.Services.Interfaces;
using MockNet04G2.Business.Services.User;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Models;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : BaseController
    {
        private readonly GetAllCampaignsService _getAllCampaignsService;
        private readonly GetCampaignByIdService _getCampaignByIdService;
        private readonly FilterCampaignsByStatusService _filterCampaignsByStatusService;
        private readonly CampaignsPagingService _campaignsPagingService;
        public CampaignsController(GetAllCampaignsService getAllCampaignsService,
            GetCampaignByIdService getCampaignByIdService,
            FilterCampaignsByStatusService filterCampaignsByStatusService,
            CampaignsPagingService campaignsPagingService)
        {
            _getAllCampaignsService = getAllCampaignsService;
            _getCampaignByIdService = getCampaignByIdService;
            _filterCampaignsByStatusService = filterCampaignsByStatusService;
            _campaignsPagingService = campaignsPagingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaignsAsync()
        {
            var result = await _getAllCampaignsService.ExecuteAsync();
            return HandleApiResponse(result);
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetCampaignByIdAsync(int id)
        {
            var result = await _getCampaignByIdService.ExecuteAsync(id);
            return HandleApiResponse(result);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> FilterCampaignsByStatusAsync(StatusEnum status)
        {
            var result = await _filterCampaignsByStatusService.ExecuteAsync(status);
            return HandleApiResponse(result);
        }

        [HttpGet("Page/{page}")]
        public async Task<IActionResult> CampaignsPagingAsync(int page, int pageSize = 9) // tạm thời để mặc định 9 item 1 page 
        {
            var result = await _campaignsPagingService.ExecuteAsync(page,pageSize);
            return HandleApiResponse(result);
        }

    }
}
