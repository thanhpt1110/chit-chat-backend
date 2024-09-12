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
    public class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> modelBuilder)
        {
            // PostMedia
            modelBuilder
            .HasKey(pm => pm.Id);

            modelBuilder
                .HasOne(pm => pm.Post)
                .WithMany()
                .HasForeignKey(pm => pm.PostId);
        }
    }
}
