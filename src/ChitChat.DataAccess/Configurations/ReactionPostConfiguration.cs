using ChitChat.Domain.Entities.ChatEntities;
using ChitChat.Domain.Entities.PostEntities.Reaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Configurations
{
    public class ReactionPostConfiguration : IEntityTypeConfiguration<ReactionPost>
    {
        public void Configure(EntityTypeBuilder<ReactionPost> modelBuilder)
        {
            modelBuilder
            .HasKey(rp => new { rp.PostId, rp.UserId });

            modelBuilder
                .HasOne(rp => rp.Post)
                .WithMany()
            .HasForeignKey(rp => rp.PostId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(rp => rp.User)
                .WithMany()
                .HasForeignKey(rp => rp.UserId);
        }
    }
}
