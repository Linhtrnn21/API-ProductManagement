using Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PM.Entities;
using PM.Model;
using PM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
            IUserService userService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (!await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                throw new UnauthorizedAccessException("Incorrect password");
            }

            var token = _userService.GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Message = "Login successful"
            };
        }

        public async Task Regiter(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                user.Role = "User";
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}