using ChitChat.Domain.Entities.PostEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class PostDetailTagConfiguration : IEntityTypeConfiguration<PostDetailTag>
    {
        public void Configure(EntityTypeBuilder<PostDetailTag> modelBuilder)
        {
            // PostDetailTag
            modelBuilder.HasKey(pdt => pdt.Id);

            modelBuilder
                .HasOne(pdt => pdt.Post)
                .WithMany(p => p.PostDetailTags)
                .HasForeignKey(pdt => pdt.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
