namespace ChitChat.Application.Models.Dtos.Post.CreatePost
{
    public class CreatePostRequestDto
    {
        public string Description { get; set; }
        public List<CreatePostMediaRequestDto> PostMedias { get; set; }
    }
}
