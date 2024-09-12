using ChitChat.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Configurations
{
    public class UserFollowerRequestConfiguration: IEntityTypeConfiguration<UserFollowerRequest>
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
