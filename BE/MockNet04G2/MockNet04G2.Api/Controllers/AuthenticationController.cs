using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.DTOs.Authentication.Requests;
using MockNet04G2.Business.Services.Authentication;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly LoginService _loginService;

        public AuthenticationController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await _loginService.ExecuteAsync(request);
            return HandleApiResponse(result);
        }
    }
}
