namespace ChitChat.Domain.Entities.UserEntities
{
    public class Profile : BaseEntity
    {
        public string UserApplicationId { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string SearchData { get; set; }
        public UserApplication UserApplication { get; set; }
    }
}
