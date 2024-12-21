using ChitChat.Domain.Entities.PostEntities.Reaction;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            .HasForeignKey(rp => rp.PostId);

            modelBuilder
                .HasOne(rp => rp.User)
                .WithMany()
                .HasForeignKey(rp => rp.UserId);
        }
    }
}
