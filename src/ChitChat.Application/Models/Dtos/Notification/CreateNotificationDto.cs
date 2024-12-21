namespace ChitChat.Application.Models.Dtos.Notification
{
    public abstract class CreateNotificationDto
    {
        public string Type { get; set; }
        public string Action { get; set; }
        public string Reference { get; set; }
        public string LastInteractorUserId { get; set; }
        public string ReceiverUserId { get; set; }
    }
}
