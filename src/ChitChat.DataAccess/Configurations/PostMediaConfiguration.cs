using ChitChat.Domain.Entities.PostEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class PostMediaConfiguration : IEntityTypeConfiguration<PostMedia>
    {
        public void Configure(EntityTypeBuilder<PostMedia> modelBuilder)
        {
            // PostMedia
            modelBuilder.HasKey(pm => pm.Id);

            modelBuilder
                .HasOne(pm => pm.Post)
                .WithMany(p => p.PostMedias)
                .HasForeignKey(pm => pm.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
