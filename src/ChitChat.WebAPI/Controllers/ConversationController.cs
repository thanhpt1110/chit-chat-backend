using ChitChat.Application.Helpers;
using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.Message;
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
        public async Task<IActionResult> GetAllConversationsAsync([FromQuery] PaginationFilter pagination)
        {
            return Ok(ApiResult<List<ConversationDto>>.Success(await _conversationService.GetAllConversationsAsync(pagination)));
        }
        [HttpGet]
        [Route("{conversationId}")]
        public async Task<IActionResult> GetConversationsByIdAsync(Guid conversationId, [FromQuery] int messagePageIndex = 0, [FromQuery] int messagePageSize = 100)
        {
            return Ok(ApiResult<ConversationDetailDto>.Success(await _conversationService.GetConversationsByIdAsync(conversationId, messagePageIndex, messagePageSize)));
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateConversationAsync([FromBody] List<string> userIds)
        {
            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.CreateConversationAsync(userIds)));
        }
        [HttpPost]
        [Route("{conversationId}")]
        public async Task<IActionResult> SendMessageAsync(Guid conversationId, [FromBody] RequestSendMessageDto request)
        {
            return Ok(ApiResult<MessageDto>.Success(await _conversationService.SendMessageAsync(conversationId, request)));
        }
        [HttpPut]
        [Route("{conversationId}")]
        public async Task<IActionResult> UpdateConversationAsync(Guid conversationId, [FromBody] ConversationDto request)
        {
            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.UpdateConversationAsync(request)));
        }
        [HttpPut]
        [Route("{conversationId}/messages/{messageId}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid conversationId, Guid messageId, [FromBody] MessageDto messageDto)
        {
            return Ok(ApiResult<MessageDto>.Success(await _conversationService.UpdateMessageAsync(messageDto)));
        }
        [HttpDelete]
        [Route("{conversationId}")]
        public async Task<IActionResult> DeleteConversationAsync(Guid conversationId)
        {
            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.DeleteConversationAsync(conversationId)));
        }
        [HttpDelete]
        [Route("{conversationId}/messages/{messageId}")]
        public async Task<IActionResult> DeleteConversationAsync(Guid conversationId, Guid messageId)
        {
            return Ok(ApiResult<MessageDto>.Success(await _conversationService.DeleteMessageAsync(messageId)));
        }
    }
}
