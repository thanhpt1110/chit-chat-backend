using ChitChat.Domain.Common;

namespace ChitChat.Application.Localization
{
    public static class ValidationTexts
    {
        public static readonly LocalizedText NotFound = LocalizedText.New("{0} {1} not found").AddDefaultText("{0} {1} không tồn tại.");
        public static readonly LocalizedText Conflict = LocalizedText.New("{0} {1} is already existed").AddDefaultText("{0} với {1} đã tồn tại.");
        public static readonly LocalizedText NotValidate = LocalizedText.New("{0} {1} is not validated").AddDefaultText("{0} với {1} không hợp lệ.");
        public static readonly LocalizedText Forbidden = LocalizedText.New("Access to {0} {1} is forbidden").AddDefaultText("Quyền truy cập {0} với {1} bị cấm.");
        public static readonly LocalizedText Unauthorized = LocalizedText.New("Unauthorized access to {0} {1}").AddDefaultText("Truy cập trái phép {0} với {1}.");
    }
}
