namespace ChitChat.Domain.Common
{
    public class ErrorDescription
    {

        public static readonly string InvalidAccessOrRefreshToken = LocalizedText.New("Invalid access token or refresh token")
                                .AddDefaultText("Mã truy cập không hợp lệ")
                                .ToString();
    }
}
