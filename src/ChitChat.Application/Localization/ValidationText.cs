using ChitChat.Domain.Common;

namespace ChitChat.Application.Localization
{
    public static class ValidationTexts
    {
        public static readonly LocalizedText NotFound = LocalizedText.New("{0} {1} not found").AddDefaultText("{0} {1} không tồn tại.");
        public static readonly LocalizedText Conflict = LocalizedText.New("{0} {1} is already existed").AddDefaultText("{0} với {1} đã tồn tại.");
        public static readonly LocalizedText NotValidate = LocalizedText.New("{0} {1} is not validated").AddDefaultText("{0} với {1} không hợp lệ.");

    }
}
