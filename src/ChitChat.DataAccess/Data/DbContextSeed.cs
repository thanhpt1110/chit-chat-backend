using ChitChat.Domain.Enums;
using ChitChat.Domain.Identity;

using Microsoft.AspNetCore.Identity;

namespace ChitChat.DataAccess.Data
{
    public static class DbContextSeed
    {
        public static async Task SeedDatabaseAsync(UserManager<UserApplication> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // Seed dữ liệu người dùng
            var hasher = new PasswordHasher<UserApplication>();
            if (!roleManager.Roles.Any())
            {
                foreach (var role in Enum.GetNames<UserRoles>())
                {
                    var applicationRole = new ApplicationRole
                    {
                        Name = role.ToString()
                    };

                    await roleManager.CreateAsync(applicationRole);
                }
            }
            if (!userManager.Users.Any())
            {
                var adminUsers = new List<UserApplication>
            {
                new UserApplication
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    DisplayName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    AvatarUrl = "",
                    UserStatus = Domain.Enums.UserStatus.Public,
                },
                new UserApplication
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin1",
                    DisplayName = "admin1",
                    Email = "admin1@gmail.com",
                    EmailConfirmed = true,
                    AvatarUrl = "",
                    UserStatus = Domain.Enums.UserStatus.Public,
                }
            };

                foreach (var user in adminUsers)
                {
                    await userManager.CreateAsync(user, "Password123!");

                    await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
                }

            }
        }
    }
}
