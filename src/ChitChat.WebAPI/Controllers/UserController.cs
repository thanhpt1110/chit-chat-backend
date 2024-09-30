using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterationRequestDto registerationRequestDto)
        {
            return Ok(ApiResult<bool>.Success(await _userService.RegisterAsync(registerationRequestDto)));
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequestDto)
        {
            return Ok(ApiResult<LoginResponseDto>.Success(await _userService.LoginAsync(loginRequestDto)));
        }
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser([FromBody] Guid loginHistoryId)
        {
            return Ok(ApiResult<bool>.Success(await _userService.LogoutAsync(loginHistoryId)));
        }
    }
}
