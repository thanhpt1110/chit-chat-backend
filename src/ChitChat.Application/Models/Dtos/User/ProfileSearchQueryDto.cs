namespace ChitChat.Application.Models.Dtos.User
{
    public class ProfileSearchQueryDto : PaginationFilter
    {
        public string SearchText { get; set; }
    }
}
