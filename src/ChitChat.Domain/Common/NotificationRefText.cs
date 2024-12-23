namespace ChitChat.Domain.Common
{
    public static class NotificationRefText
    {
        public static string PostRef(Guid postId)
        {
            return $"Post/{postId}";
        }
        public static string UserRef(string userId)
        {
            return $"User/{userId}";
        }
        public static string CommentRef(Guid postId, Guid commentId)
        {
            return $"Post/{postId}/Comment/{commentId}";
        }
    }
}
