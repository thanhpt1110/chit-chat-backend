using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IConversationService _conversationService;
        public ConversationController(ILogger logger, IConversationService conversationService)
        {
            _logger = logger;
            _conversationService = conversationService;
        }
        [HttpGet]
        [Route("/user/{userId}")]
        public IActionResult GetConverastionByUserId(string userId)
        {
            return null;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CreateConversationRequestDto request)
        {
            return Ok(ApiResult<ConversationDto>.Success(await _conversationService.CreateNewConversation(request)));
        }
    }
}
