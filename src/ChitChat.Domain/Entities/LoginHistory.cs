namespace ChitChat.Domain.Entities
{
    public class LoginHistory:BaseEntity
    {
        public string UserId { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
