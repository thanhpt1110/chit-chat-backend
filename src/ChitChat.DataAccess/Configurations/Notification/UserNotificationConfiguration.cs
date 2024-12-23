using ChitChat.Domain.Entities.SystemEntities.Notification;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChitChat.DataAccess.Configurations.Notification
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.HasKey(n => n.Id);
            builder.HasOne(n => n.ReceiverUser)
                .WithMany()
                .HasForeignKey(n => n.ReceiverUserId);
            builder.HasOne(n => n.LastInteractorUser)
                .WithMany()
                .HasForeignKey(n => n.LastInteractorUserId);

        }
    }
}
