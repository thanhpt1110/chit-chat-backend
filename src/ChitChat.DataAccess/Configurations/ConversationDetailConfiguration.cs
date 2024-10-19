using ChitChat.Domain.Entities.ChatEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class ConversationDetailConfiguration : IEntityTypeConfiguration<ConversationDetail>
    {
        public void Configure(EntityTypeBuilder<ConversationDetail> modelBuilder)
        {
            // Conversation Detail
            modelBuilder
            .HasKey(c => c.Id);

            modelBuilder
                .HasOne(m => m.Conversation)
                .WithMany(c => c.ConversationDetails)
                .HasForeignKey(m => m.ConversationId)
                 .OnDelete(DeleteBehavior.Restrict);
            ;
        }
    }
}
