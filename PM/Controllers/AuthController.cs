using Application.AppServices;
using Microsoft.AspNetCore.Mvc;
using PM.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await _authService.Regiter(model);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception($"Message: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs in a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var token = await _authService.Login(model);
                return Ok(token);
            }
            catch (KeyNotFoundException ex)
            {
                return Unauthorized($"Message: {ex.Message}");
            }
        }
    }
}