using ChitChat.Domain.Entities.UserEntities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> modelBuilder)
        {
            // UserFollower
            modelBuilder
            .HasKey(uf => uf.Id);

            /*       modelBuilder
                   .Ignore(ufr => ufr.User);

                   modelBuilder
                   .Ignore(ufr => ufr.Follower);*/
            modelBuilder
                .HasOne(uf => uf.User)
                .WithMany()
                .HasForeignKey(uf => uf.UserId);
            modelBuilder
                .HasOne(uf => uf.Follower)
                .WithMany()
                .HasForeignKey(uf => uf.FollowerId);
        }
    }
}
