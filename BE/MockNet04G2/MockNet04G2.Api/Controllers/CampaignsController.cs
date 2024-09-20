using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.DTOs.Campaign.Requests;
using MockNet04G2.Business.DTOs.Campaign.Responses;
using MockNet04G2.Business.Services.Campagin;
using MockNet04G2.Business.Services.Campaign;
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
        private readonly GetTotalCampaignsService _getTotalCampaignsService;
        private readonly AddCampaignService _addCampaignService;
        private readonly DeleteCampaignService _deleteCampaignService;
        private readonly SearchCampaignService _searchCampaignService;
        private readonly GetCampaignsCountAfterFilterService _getCampaignsCountAfterFilterService;
        private readonly UpdateCampaignService _updateCampaignService;
        private readonly GetHomePageCampaignService _getHomePageCampaignService;
        private readonly GetHomePageCampaignCountService _getHomePageCampaignCountService;
        public CampaignsController(GetAllCampaignsService getAllCampaignsService,
            GetCampaignByIdService getCampaignByIdService,
            FilterCampaignsByStatusService filterCampaignsByStatusService,
            CampaignsPagingService campaignsPagingService,
            GetTotalCampaignsService getTotalCampaignsService,
            AddCampaignService addCampaignService,
            DeleteCampaignService deleteCampaignService,
            SearchCampaignService searchCampaignService,
            GetCampaignsCountAfterFilterService getCampaignsCountAfterFilterService,
             UpdateCampaignService updateCampaignService,
             GetHomePageCampaignService getHomePageCampaignService,
             GetHomePageCampaignCountService getHomePageCampaignCountService)
        {
            _getAllCampaignsService = getAllCampaignsService;
            _getCampaignByIdService = getCampaignByIdService;
            _filterCampaignsByStatusService = filterCampaignsByStatusService;
            _campaignsPagingService = campaignsPagingService;
            _getTotalCampaignsService = getTotalCampaignsService;
            _addCampaignService = addCampaignService;
            _deleteCampaignService = deleteCampaignService;
            _searchCampaignService = searchCampaignService;
            _getCampaignsCountAfterFilterService = getCampaignsCountAfterFilterService;
            _updateCampaignService = updateCampaignService;
            _getHomePageCampaignService = getHomePageCampaignService;
            _getHomePageCampaignCountService = getHomePageCampaignCountService;
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

        [HttpGet("Status/{status}/{page}")]
        public async Task<IActionResult> FilterCampaignsByStatusAsync(StatusEnum status,int page = 1) 
        {
            int pageSize = 9;// tạm thời để mặc định pagesize = 9 
            var result = await _filterCampaignsByStatusService.ExecuteAsync(status,page, pageSize);
            return HandleApiResponse(result);
        }

        [HttpGet("TotalCampaignsCountAfterFilter/{status}")]
        public async Task<IActionResult> TotalCampaignsCountAfterFilter(StatusEnum status)
        {
            var result = await _getCampaignsCountAfterFilterService.ExecuteAsync(status);
            return HandleApiResponse(result);
        }

        [HttpGet("Page/{page}")]
        public async Task<IActionResult> CampaignsPagingAsync(int page) 
        {
            int pageSize = 9; // tạm thời để mặc định 9 item 1 page 
            var result = await _campaignsPagingService.ExecuteAsync(page,pageSize);
            return HandleApiResponse(result);
        }

        [HttpGet("TotalCampaignsCount")]
        public async Task<IActionResult> TotalCampaignsCount()
        {
            var result = await _getTotalCampaignsService.ExecuteAsync();
            return HandleApiResponse(result);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Campaign")]
        public async Task<IActionResult> AddCampaignAsync(CampaignDetailRequest request)
        {
            var result = await _addCampaignService.ExecuteAsync(request);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Campaign")]
        public async Task<IActionResult> DeleteCampaignAsync(int campaignId)
        {
            var result = await _deleteCampaignService.ExecuteAsync(campaignId);
            return HandleApiResponse(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchCampaignAsync(string campaignCode, string organizationPhone)
        {
            var result = await _searchCampaignService.ExecuteAsync(campaignCode, organizationPhone);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update-Campaign/{id}")]
        public async Task<IActionResult> UpdateCampaignAsync(int id,UpdateCampaignRequest request)
        {
            var result = await _updateCampaignService.ExecuteAsync(id,request);
            return HandleApiResponse(result);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterCampaignAsync(
        [FromQuery] int pageSize = 9,
        [FromQuery] int page = 1,
        [FromQuery] string code = "",
        [FromQuery] string phone = "",
        [FromQuery] StatusEnum? status = null)
        {
            var result = await _getHomePageCampaignService.ExecuteAsync(pageSize, page, code, phone, status);
            return HandleApiResponse(result);
        }

        [HttpGet("FilterCount")]
        public async Task<IActionResult> FilterCampaignCountAsync(
        [FromQuery] string code = "",
        [FromQuery] string phone = "",
        [FromQuery] StatusEnum? status = null)
        {
            var result = await _getHomePageCampaignCountService.ExecuteAsync(code, phone, status);
            return HandleApiResponse(result);
        }
    }
}
