namespace chit_chat_backend.Domain.Entities.PostEntities
{
    public class PostMedia:BaseEntity
    {
        public Guid PostId { get; set; }
        public string MediaType { get; set; }
        public string MediaUrl { get; set; }
        public int MediaOrder { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public Post Post { get; set; } // Navigation property
    }
}
