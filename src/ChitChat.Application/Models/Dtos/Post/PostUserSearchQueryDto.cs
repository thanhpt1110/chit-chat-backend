namespace ChitChat.Application.Models.Dtos.Post
{
    public class PostUserSearchQueryDto : PaginationFilter
    {
        public string? UserId { get; set; }
    }
}
