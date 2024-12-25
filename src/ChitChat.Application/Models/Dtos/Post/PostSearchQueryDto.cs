namespace ChitChat.Application.Models.Dtos.Post
{
    public class PostSearchQueryDto : PaginationFilter
    {
        public string SearchText { get; set; } = string.Empty;
        public bool IsTag { get; set; } = false;

    }
}
