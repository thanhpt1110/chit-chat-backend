namespace ChitChat.Domain.Common
{
    public static class NotificationFormatter
    {
        public static string FormatPostLikeNotification(string userAvatar, string userName, string postImage, string postDescription, DateTime dateTimeLike)
        {
            return $"{userAvatar} ({userName}) liked your post. ({postImage}, {postDescription}) - {dateTimeLike:yyyy-MM-dd HH:mm:ss}";
        }

        public static string FormatPostCommentNotification(string userAvatar, string userName, string postImage, string postDescription, DateTime dateTimeComment)
        {
            return $"{userAvatar} ({userName}) commented on your post. ({postImage}, {postDescription}) - {dateTimeComment:yyyy-MM-dd HH:mm:ss}";
        }

        public static string FormatCommentLikeNotification(string userAvatar, string userName, string myCommentContent, string postImage, DateTime dateTimeLike)
        {
            return $"{userAvatar} ({userName}) liked your comment: \"{myCommentContent}\" - ({postImage}) - {dateTimeLike:yyyy-MM-dd HH:mm:ss}";
        }

        public static string FormatCommentReplyNotification(string userAvatar, string userName, string myCommentContent, string postImage, DateTime dateTimeComment)
        {
            return $"{userAvatar} ({userName}) replied to your comment: \"{myCommentContent}\" - ({postImage}) - {dateTimeComment:yyyy-MM-dd HH:mm:ss}";
        }

        public static string FormatFollowerNotification(string userAvatar, string userName, DateTime dateTimeFollow)
        {
            return $"{userAvatar} ({userName}) started following you - {dateTimeFollow:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
