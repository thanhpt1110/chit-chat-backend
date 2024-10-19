using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> modelBuilder)
        {
            // Configure primary key
            modelBuilder.HasKey(m => m.Id);

            // Configure relationship with Conversation
            modelBuilder
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            // Configure relationship with Sender
            modelBuilder
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId);
        }
    }
}
