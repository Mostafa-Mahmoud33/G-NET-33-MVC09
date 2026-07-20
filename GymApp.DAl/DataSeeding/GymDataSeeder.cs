using GymApp.DAl.Context;
using Microsoft.Extensions.Logging;
using System.Text.Json;
namespace GymApp.DAl.DataSeeding
{
    public class GymDataSeeder
    {
        // SeedAsync
        // Check If the table is empty
        // Reads Data From JSON files and seeds the database with initial data.

        // Gets called on the application startup
        public static async Task SeedAsync(GymDbContext dbContext, 
            string folderPath,
            ILogger logger,
            CancellationToken cancellationToken = default!)
        {
            // file name = plans.json
            try
            {
                if (!await dbContext.Plans.AnyAsync(cancellationToken))
                {
                    var plans = await LoadDataFromJsonAsync(folderPath, "plans.json", cancellationToken);

                    if (plans is not null && plans.Any())
                    {
                        dbContext.Plans.AddRange(plans);
                        logger.LogInformation($"Seeded {plans.Count} plans");
                    }
                    if (dbContext.ChangeTracker.HasChanges())
                        await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                // log in All config providers
                // Console + Output + Event Viewer
                logger.LogError(ex, "Seeding Failed");
                throw;
            }
        }

        private static async Task<List<Plan>?> LoadDataFromJsonAsync(string folderPath,string fileName, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(folderPath, fileName);

            // read file data => C# Objects
            var plansJson = await File.ReadAllTextAsync(filePath, cancellationToken);
            // JSON => C# => Deserialization
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            var plans = JsonSerializer.Deserialize<List<Plan>>(plansJson, options);
            return plans;
        }
    }
}
