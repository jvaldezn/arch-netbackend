using Data.Configuration.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Extensions
{
    public static class DatabaseInitializerExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
                logger.LogError(ex, "Error aplicando migraciones en AppDbContext.");
            }

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<LogDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<LogDbContext>>();
                logger.LogError(ex, "Error aplicando migraciones en LogDbContext.");
            }

            return app;
        }
    }
}
