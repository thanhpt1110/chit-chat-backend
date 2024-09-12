using ChitChat.Domain.Entities.ChatEntities;
using ChitChat.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> modelBuilder)
        {
            // Comment
            modelBuilder
            .HasKey(c => c.Id);

            modelBuilder
                .HasOne(c => c.Post)
                .WithMany(p=>p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder
                .HasOne(c => c.ParentComment)
                .WithMany()
                .HasForeignKey(c => c.ParentCommentId)
                 .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
