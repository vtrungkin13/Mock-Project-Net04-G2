﻿using Microsoft.AspNetCore.Authorization;
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
using MockNet04G2.Business.DTOs.Campaign.Requests;
using MockNet04G2.Business.Services.Campaign;
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
        private readonly UpdateUserService _updateUserService;
        private readonly FilterUserCountService _filterUserCountService;
        private readonly FilterService _filterService;


        public UserController(GetAllUserService getAllUserService, FindUserService findUserService,
            ChangeUserRoleService changeUserRoleService,
            ChangePasswordService changePasswordService,
            UsersPagingService usersPagingService,
            CountUserService countUserService,
            UpdateUserService updateUserService, 
            FilterUserCountService filterUserCountService, 
            FilterService filterService)
        {
            _getAllUserService = getAllUserService;
            _findUserService = findUserService;
            _changeUserRoleService = changeUserRoleService;
            _changePasswordService = changePasswordService;
            _usersPagingService = usersPagingService;
            _countUserService = countUserService;
            _updateUserService = updateUserService;
            _filterUserCountService = filterUserCountService;
            _filterService = filterService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _getAllUserService.ExecuteAsync();
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var result = await _findUserService.ExecuteAsync(name);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Role/{id}")]
        public async Task<IActionResult> ChangeUserRole(int id, [FromBody] RoleEnum newRole)
        {
            var result = await _changeUserRoleService.ExecuteAsync(id, newRole);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("Password/{userId}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request, int userId)
        {
            var result = await _changePasswordService.ExecuteAsync(request, userId);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Page/{page}")]
        public async Task<IActionResult> UsersPagingAsync(int page, int pageSize = 9)
        {
            var result = await _usersPagingService.ExecuteAsync(page, pageSize);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Count")]
        public async Task<IActionResult> CountUserAsync()
        {
            var result = await _countUserService.ExecuteAsync();
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _findUserService.ExecuteAsync(id);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("Update-User/{id}")]
        public async Task<IActionResult> UpdateUsernAsync(int id, UpdateUserRequest request)
        {
            var result = await _updateUserService.ExecuteAsync(id, request);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Filter/{pageSize}/{page}/{name}")]
        public async Task<IActionResult> FilterAsync(int page = 1, int pageSize = 9, string name = "")
        {
            var result = await _filterService.ExecuteAsync(pageSize, page, name);
            return HandleApiResponse(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("FilterCount/{name}")]
        public async Task<IActionResult> FilterCountAsync(string name = "")
        {
            var result = await _filterUserCountService.ExecuteAsync(name);
            return HandleApiResponse(result);
        }
    }
}
