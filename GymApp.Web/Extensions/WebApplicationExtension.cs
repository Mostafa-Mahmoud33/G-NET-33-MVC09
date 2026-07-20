using GymApp.DAl.Context;
using GymApp.DAl.DataSeeding;
using GymApp.DAl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Web.Extensions
{
    public static class WebApplicationExtension
    {
        public static async Task<WebApplication> MigrateAndSeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            //DBContext
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
            // Logger
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            // Migrate DB

            var pending = await dbContext.Database.GetPendingMigrationsAsync();
            if (pending.Any())
            {
                logger.LogInformation("Applying {count} Pending Migrations", pending.Count());
                await dbContext.Database.MigrateAsync();
            }

            var folderPath = Path.Combine("wwwroot", "Files");
            await GymDataSeeder.SeedAsync(dbContext, folderPath, logger);
            await IdentityDataSeeder.SeedAsync(userManager, roleManager, logger);
            return app;
        }
    }
}
