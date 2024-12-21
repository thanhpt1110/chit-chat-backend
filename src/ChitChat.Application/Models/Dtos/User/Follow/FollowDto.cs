namespace ChitChat.Application.Models.Dtos.User.Follow
{
    public class FollowDto : BaseResponseDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; }
    }
}
