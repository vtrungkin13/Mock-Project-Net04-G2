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
        private readonly ResetPasswordService _resetPasswordService;

        public AuthenticationController(LoginService loginService, RegisterService registerService, ResetPasswordService resetPasswordService)
        {
            _loginService = loginService;
            _registerService = registerService;
            _resetPasswordService = resetPasswordService;
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

        [HttpPut("reset/{email}")]
        public async Task<IActionResult> ResetPasswordAsync(string email)
        {
            var result = await _resetPasswordService.ExecuteAsync(email);
            return HandleApiResponse(result);
        }
    }
}
