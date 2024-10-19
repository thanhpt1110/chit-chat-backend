using ChitChat.Application.Helpers;
using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly ILogger<ConversationController> _logger;
        private readonly IConversationService _conversationService;
        private readonly IClaimService _claimService;
        public ConversationController(ILogger<ConversationController> logger, IConversationService conversationService, IClaimService claimService)
        {
            _logger = logger;
            _conversationService = conversationService;
            _claimService = claimService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetConverastionByUserId([FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 15)
        {
            var userId = _claimService.GetUserId();
            return Ok(ApiResult<List<ConversationDto>>.Success(await _conversationService.GetConversationsByUserId(userId)));
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(string recieverId)
        {

            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.CreateNewConversation(recieverId, _claimService.GetUserId())));
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put(ConversationDto conversationDto)
        {
            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.UpdateConversation(conversationDto)));
        }
        [HttpDelete]
        [Route("{conversaionId}")]
        public async Task<IActionResult> Delete(Guid conversaionId)
        {
            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.DeleteConversation(conversaionId)));
        }
    }
}
