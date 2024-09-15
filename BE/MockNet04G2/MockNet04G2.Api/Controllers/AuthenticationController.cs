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
        private readonly RegisterService _registerService;

        public AuthenticationController(LoginService loginService, RegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var result = await _loginService.ExecuteAsync(request);
            return HandleApiResponse(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var result = await _registerService.ExecuteAsync(request);
            return HandleApiResponse(result);
        }
    }
}
