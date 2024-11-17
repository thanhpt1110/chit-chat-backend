namespace ChitChat.Application.Models.Dtos.User.Profile
{
    public class ProfileSearchQueryDto : PaginationFilter
    {
        public string SearchText { get; set; }
    }
}
