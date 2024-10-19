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
                    FirstName = "admin",
                    LastName = "1",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    AvatarUrl = "",
                    Bio = "",
                    UserStatus = Domain.Enums.UserStatus.Public,
                    Gender = "Male"
                },
                new UserApplication
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin1",
                    FirstName = "admin1",
                    LastName = "2",
                    Email = "admin1@gmail.com",
                    EmailConfirmed = true,
                    AvatarUrl = "",
                    Bio = "",
                    UserStatus = Domain.Enums.UserStatus.Public,
                    Gender = "Male"
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
