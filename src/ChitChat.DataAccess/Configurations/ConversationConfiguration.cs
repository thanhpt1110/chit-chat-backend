using ChitChat.Domain.Entities.ChatEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> modelBuilder)
        {
            // Conversation Detail
            modelBuilder
            .HasKey(c => c.Id);

            modelBuilder
                .HasOne(m => m.LastMessage)
                .WithMany()
                .HasForeignKey(m => m.LastMessageId)
                 .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
