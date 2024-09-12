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
