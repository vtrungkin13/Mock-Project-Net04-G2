using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.Services.Interfaces;
using MockNet04G2.Core.Models;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly IBaseService<Campaign> _campaignService;
        public CampaignsController(IBaseService<Campaign> campaignService)
        {
            _campaignService = campaignService;
        }
    }
}
