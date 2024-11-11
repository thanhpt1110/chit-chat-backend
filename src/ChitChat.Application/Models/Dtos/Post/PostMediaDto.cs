namespace ChitChat.Application.Models.Dtos.Post
{
    public class PostMediaDto : BaseResponseDto
    {
        public Guid PostId { get; set; }
        public string MediaType { get; set; }
        public string MediaUrl { get; set; }
        public int MediaOrder { get; set; }
        public string Description { get; set; }
    }
}
