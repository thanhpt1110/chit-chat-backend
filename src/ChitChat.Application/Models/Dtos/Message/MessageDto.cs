namespace ChitChat.Application.Models.Dtos.Message
{
    public class MessageDto
    {
        Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string UserSenderId { get; set; }
        public Guid ConverastionId { get; set; }
    }
}
