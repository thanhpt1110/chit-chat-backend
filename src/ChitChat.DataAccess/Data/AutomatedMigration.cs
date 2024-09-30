using ChitChat.DataAccess.Data;
using ChitChat.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UCA.DataAccess.Persistence
{
    public static class AutomatedMigration
    {
        public static async Task MigrateAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            var database = context.Database;

            var pendingMigrations = await database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await database.MigrateAsync();
            }

            var userManager = services.GetRequiredService<UserManager<UserApplication>>();

            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            await DbContextSeed.SeedDatabaseAsync(userManager, roleManager);
        }
    }
}