using ChitChat.Domain.Entities.ChatEntities;
using ChitChat.Domain.Entities.SystemEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Configurations
{
    public class UserInteractionConfiguration : IEntityTypeConfiguration<UserInteraction>
    {
        public void Configure(EntityTypeBuilder<UserInteraction> modelBuilder)
        {
            // UserInteraction
            modelBuilder
                .HasKey(ui => ui.Id);

            modelBuilder
                .HasOne(ui => ui.User)
                .WithMany()
            .HasForeignKey(ui => ui.UserId);

            modelBuilder
                .HasOne(ui => ui.Post)
                .WithMany()
                .HasForeignKey(ui => ui.PostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
