using ChitChat.Domain.Entities.SystemEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> modelBuilder)
        {
            // Notification
            modelBuilder
            .HasKey(n => n.Id);

            modelBuilder
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId);
        }
    }
}
