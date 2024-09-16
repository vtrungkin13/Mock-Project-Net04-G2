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
    public class CampaignsController : ControllerBase
    {
        private readonly GetAllCampaignsService _getAllCampaignsService;
        private readonly GetCampaignByIdService _getCampaignByIdService;
        private readonly FilterCampaignsByStatusService _filterCampaignsByStatusService;
        public CampaignsController(GetAllCampaignsService getAllCampaignsService,
            GetCampaignByIdService getCampaignByIdService,
            FilterCampaignsByStatusService filterCampaignsByStatusService)
        {
            _getAllCampaignsService = getAllCampaignsService;
            _getCampaignByIdService = getCampaignByIdService;
            _filterCampaignsByStatusService = filterCampaignsByStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaignsAsync()
        {
            var result = await _getAllCampaignsService.ExecuteAsync();
            return Ok(result);
        }

        [HttpGet("/Detail/{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var result = await _getCampaignByIdService.ExecuteAsync(id);
            return Ok(result);
        }

        [HttpGet("{status}")]
        public async Task<IActionResult> FilterCampaignsByStatus(StatusEnum status)
        {
            var result = await _filterCampaignsByStatusService.ExecuteAsync(status);
            return Ok(result);
        }

    }
}
