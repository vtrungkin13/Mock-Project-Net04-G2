using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.Services.Campagin;
using MockNet04G2.Business.Services.Donate;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonateController : BaseController
    {
        private readonly GetDonationHistoryService _getDonationHistoryService;

        public DonateController(GetDonationHistoryService getDonationHistoryService)
        {
            _getDonationHistoryService = getDonationHistoryService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("Donate/{userId}")]
        public async Task<IActionResult> GetDonationHistory(int userId)
        {   
            var result = await _getDonationHistoryService.ExecuteAsync(userId);
            return HandleApiResponse(result);
        }

    }
}
