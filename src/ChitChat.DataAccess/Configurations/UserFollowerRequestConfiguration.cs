using ChitChat.Domain.Entities.UserEntities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class UserFollowerRequestConfiguration : IEntityTypeConfiguration<UserFollowerRequest>
    {
        public void Configure(EntityTypeBuilder<UserFollowerRequest> modelBuilder)
        {
            // UserFollowerRequest
            modelBuilder
                .HasKey(u => u.Id);
            modelBuilder
            .Ignore(ufr => ufr.User);
            modelBuilder
            .Ignore(ufr => ufr.Follower);
            //modelBuilder
            //    .HasOne(u => u.User)
            //    .WithMany()
            //.HasForeignKey(u => u.UserId);

            //modelBuilder
            //    .HasOne(u => u.Follower)
            //    .WithMany()
            //    .HasForeignKey(u => u.FollowerId);
        }
    }
}
