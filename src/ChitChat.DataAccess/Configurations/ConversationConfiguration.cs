using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> modelBuilder)
        {
            // Configure primary key
            modelBuilder.HasKey(c => c.Id);

            // Configure relationship with LastMessage
            modelBuilder
                .HasOne(c => c.LastMessage)
                .WithMany()
                .HasForeignKey(c => c.LastMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship with Messages
            modelBuilder
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with ConversationDetails
            modelBuilder
                .HasMany(c => c.ConversationDetails)
                .WithOne(cd => cd.Conversation)
                .HasForeignKey(cd => cd.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
