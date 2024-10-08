using ChitChat.Domain.Common;

namespace ChitChat.Application.Localization
{
    public static class ErrorTexts
    {
        public static readonly string InvalidAccessOrRefreshToken = LocalizedText.New("Invalid access token or refresh token")
                        .AddDefaultText("Mã truy cập không hợp lệ")
                        .ToString();
    }
}
