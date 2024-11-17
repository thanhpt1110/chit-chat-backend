using ChitChat.Domain.Entities.PostEntities;

namespace ChitChat.Application.Models.Dtos.Post
{
    public class PostDto : BaseResponseDto
    {
        public string Description { get; set; }
        public int ReactionCount { get; set; }
        public int CommentCount { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostMediaDto>? PostMedias { get; set; }
    }
}
