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
    public class PostDetailTagConfiguration : IEntityTypeConfiguration<PostDetailTag>
    {
        public void Configure(EntityTypeBuilder<PostDetailTag> modelBuilder)
        {
            // PostDetailTag
            modelBuilder
                .HasKey(pdt => pdt.Id);

            modelBuilder
                .HasOne(pdt => pdt.Post)
                .WithMany()
                .HasForeignKey(pdt => pdt.PostId);
        }
    }
}
