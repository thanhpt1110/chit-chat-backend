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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProfilesAsync([FromQuery] string searchText = "", [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 50)
        {
            return Ok(ApiResult<List<ProfileDto>>.Success(await _profileService.GetAllProfilesAsync(searchText, pageIndex, pageSize)));
        }
        [HttpGet]
        [Route("${userId}")]
        public async Task<IActionResult> GetProfileByIdAsync(Guid userId)
        {
            return Ok(ApiResult<ProfileDto>.Success(await _profileService.GetProfileByIdAsync(userId)));
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreatProfileeAsync([FromBody] ProfileRequestDto request)
        {
            return Ok(ApiResult<ProfileDto>.Success(await _profileService.CreatProfileAsync(request)));
        }
        [HttpPut]
        [Route("${userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfileByIdAsync(Guid userId, [FromBody] ProfileDto Profile)
        {
            return Ok(ApiResult<ProfileDto>.Success(await _profileService.UpdateProfileAsync(userId, Profile)));
        }
    }
}