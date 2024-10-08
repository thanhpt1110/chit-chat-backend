namespace ChitChat.Application.Models.Dtos.User
{
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
