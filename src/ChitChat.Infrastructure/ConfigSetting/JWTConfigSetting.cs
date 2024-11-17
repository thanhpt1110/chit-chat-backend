namespace ChitChat.Infrastructure.ConfigSetting
{
    public class JWTConfigSetting
    {
        public string SecretKey { get; set; } = string.Empty;
        public int TokenValidityInDays { get; set; } = 0;
        public int RefreshTokenValidityInDays { get; set; } = 0;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public static readonly string LoginHistoryIdClaimType = "LoginId";

    }
}
