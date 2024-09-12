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
    public class ReactionCommentConfiguration : IEntityTypeConfiguration<ReactionComment>
    {
        public void Configure(EntityTypeBuilder<ReactionComment> modelBuilder)
        {
            modelBuilder
            .HasKey(rc => new { rc.CommentId, rc.UserId });

            modelBuilder
                .HasOne(rc => rc.Comment)
                .WithMany()
                .HasForeignKey(rc => rc.CommentId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(rc => rc.User)
                .WithMany()
                .HasForeignKey(rc => rc.UserId);

        }
    }
}
