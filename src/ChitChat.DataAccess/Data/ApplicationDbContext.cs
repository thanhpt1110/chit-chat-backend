using System.Reflection;

using ChitChat.Domain.Entities;
using ChitChat.Domain.Entities.ChatEntities;
using ChitChat.Domain.Entities.PostEntities;
using ChitChat.Domain.Entities.PostEntities.Reaction;
using ChitChat.Domain.Entities.SystemEntities;
using ChitChat.Domain.Entities.SystemEntities.Notification;
using ChitChat.Domain.Entities.UserEntities;
using ChitChat.Domain.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChitChat.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApplication, ApplicationRole, string>
    {
        public DbSet<UserApplication> UserApplications { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<UserFollowerRequest> UserFollowerRequests { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationDetail> ConversationDetails { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostMedia> PostMedias { get; set; }
        public DbSet<PostDetailTag> PostDetailTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReactionPost> ReactionPosts { get; set; }
        public DbSet<ReactionComment> ReactionComments { get; set; }
        public DbSet<UserInteraction> UserInteractions { get; set; }
        public DbSet<PostNotification> PostNotifications { get; set; }
        public DbSet<CommentNotification> CommentNotifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public DbSet<Profile> Profiles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
