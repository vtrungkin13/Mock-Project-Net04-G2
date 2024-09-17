using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.DTOs.Authentication.Responses;
using MockNet04G2.Business.Services.User;
using MockNet04G2.Core.Common;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.Models;
using MockNet04G2.Business.Services.Interfaces;
using MockNet04G2.Business.Services;
using MockNet04G2.Business.DTOs.Users.Requests;
using MockNet04G2.Business.Services.Campagin;
namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly GetAllUserService _getAllUserService;
        private readonly FindUserService _findUserService;
        private readonly ChangeUserRoleService _changeUserRoleService;
        private readonly ChangePasswordService _changePasswordService;
        private readonly UsersPagingService _usersPagingService;
        private readonly CountUserService _countUserService;

        public UserController(GetAllUserService getAllUserService, FindUserService findUserService, 
            ChangeUserRoleService changeUserRoleService,
            ChangePasswordService changePasswordService, 
            UsersPagingService usersPagingService,
            CountUserService countUserService)
        {
            _getAllUserService = getAllUserService;
            _findUserService = findUserService;
            _changeUserRoleService = changeUserRoleService;
            _changePasswordService = changePasswordService;
            _usersPagingService = usersPagingService;
            _countUserService = countUserService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _getAllUserService.ExecuteAsync();
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var result = await _findUserService.ExecuteAsync(name);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> ChangeUserRole(int id, [FromBody] RoleEnum newRole)
        {
            var result = await _changeUserRoleService.ExecuteAsync(id, newRole);
            return HandleApiResponse(result);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, int userId)
        {
            var result = await _changePasswordService.ExecuteAsync(request, userId);
            return HandleApiResponse(result);
        }

        [HttpGet("Page/{page}")]
        public async Task<IActionResult> CampaignsPagingAsync(int page, int pageSize = 9) 
        {
            var result = await _usersPagingService.ExecuteAsync(page, pageSize);
            return HandleApiResponse(result);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> CountUserAsync()
        {
            var result = await _countUserService.ExecuteAsync();
            return HandleApiResponse(result);
        }
    }
}
