﻿using Application.AppServices;
using Application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PM.Entities;
using PM.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
        IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _userService = userService;
    }

    /// <summary>
    /// Update role for user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update-role")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateRoleRequest request)
    {
        await _userService.UpdateUserAsync(request);
        return Ok();
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteUser([FromRoute]string email)
    {
        await _userService.DeleteUserAsync(email);
        return Ok();
    }   
}
