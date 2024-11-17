namespace ChitChat.Application.Models.Dtos.Post
{
    public class PostSearchQueryDto : PaginationFilter
    {
        public string? UserId { get; set; }
        public string? SearchText { get; set; }
        public string? Tags { get; set; }
    }
}
