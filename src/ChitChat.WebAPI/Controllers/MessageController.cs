using ChitChat.Application.Helpers;
using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Services.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IClaimService _claimService;
        public MessageController(IMessageService messageService, IClaimService claimService)
        {
            _claimService = claimService;
            _messageService = messageService;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostSendMessage([FromBody] RequestSendMessageDto request)
        {
            return Ok(ApiResult<MessageDto>.Success(await _messageService.SendMessage(request, _claimService.GetUserId())));
        }
        [HttpGet]
        [Route("conversation/{conversationId}")]
        public async Task<IActionResult> GetMessagesByUserId(Guid conversationId, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 100)
        {
            return Ok(ApiResult<List<MessageDto>>.Success(await _messageService.GetMessagesByConversationId(conversationId, pageNumber, pageSize)));
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateMessage([FromBody] MessageDto request)
        {
            return Ok(ApiResult<MessageDto>.Success(await _messageService.UpdateMessage(request)));
        }
        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchMessages([FromBody] RequestSearchMessageDto request)
        {
            return Ok(ApiResult<List<MessageDto>>.Success(await _messageService.FindMessageWithText(request)));
        }

    }
}
