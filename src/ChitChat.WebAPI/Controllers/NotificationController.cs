using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Notification;
using ChitChat.Application.Services.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            this._notificationService = notificationService;
        }
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ApiResult<List<NotificationDto>>), StatusCodes.Status200OK)] // OK với ProductResponse
        public async Task<IActionResult> GetAllNotificationsAsync([FromQuery] PaginationFilter query)
        {
            return Ok(ApiResult<List<NotificationDto>>.Success(await _notificationService.GetAllNotificationsAsync(query)));
        }
    }
}
