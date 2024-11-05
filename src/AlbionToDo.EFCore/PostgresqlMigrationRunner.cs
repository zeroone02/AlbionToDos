using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlbionToDo.EFCore;
public static class PostgresqlMigrationRunner
{
    public static IServiceCollection AddPostgresqlMigrationRunner(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var serviceProvider = services
            .AddDbContext<AlbionToDoDbContext>(options =>
                options.UseNpgsql(connectionString))
            .BuildServiceProvider(false);

        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AlbionToDoDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        return services;
    }

}
