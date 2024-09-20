using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.Services.Organization;
using MockNet04G2.Core.Repositories.Interfaces;

namespace MockNet04G2.Api.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
           _organizationService = organizationService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrganizationsAsync()
        {
            var result = await _organizationService.ExecuteAsync();
            return HandleApiResponse(result);
        }
    }
}
