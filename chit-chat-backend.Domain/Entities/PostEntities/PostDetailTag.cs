namespace chit_chat_backend.Domain.Entities.PostEntities
{
    public class PostDetailTag:BaseEntity
    {
        public Guid PostId { get; set; }
        public string Tag { get; set; }
        public bool IsDeleted { get; set; }
        public Post Post { get; set; } // Navigation property
    }
}
