namespace ChitChat.Domain.Entities.SystemEntities.Notification
{
    public class UserNotification : Notification
    {
        public UserNotification()
        {
            Type = NotificationType.User.ToString();
        }
    }
}
