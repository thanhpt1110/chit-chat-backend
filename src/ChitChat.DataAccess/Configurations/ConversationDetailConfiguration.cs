using ChitChat.Domain.Entities.ChatEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
                .WithMany()
                .HasForeignKey(m => m.ConversationId);

        }
    }
}
