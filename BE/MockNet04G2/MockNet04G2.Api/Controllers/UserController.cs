using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.DTOs.Authentication.Responses;
using MockNet04G2.Business.Services.User;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Repositories.Interfaces;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly GetAllUserService _getAllUserService;

        public UserController(GetAllUserService getAllUserService)
        {
            _getAllUserService = getAllUserService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _getAllUserService.ExecuteAsync();
            return Ok(result);
        }
    }
}
