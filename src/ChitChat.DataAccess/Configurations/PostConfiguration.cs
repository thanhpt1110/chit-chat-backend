using ChitChat.Domain.Entities.PostEntities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> modelBuilder)
        {
            // Post
            modelBuilder
            .HasKey(p => p.Id);

            // Configure relationship with User 
            modelBuilder
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            // Configure relationship with Comments
            modelBuilder
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with PostDetailTags
            modelBuilder
                .HasMany(p => p.PostDetailTags)
                .WithOne(pdt => pdt.Post)
                .HasForeignKey(pdt => pdt.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with PostMedias
            modelBuilder
                .HasMany(p => p.PostMedias)
                .WithOne(pm => pm.Post)
                .HasForeignKey(pm => pm.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
