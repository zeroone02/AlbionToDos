using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlbionToDo.EFCore;
public class AlbionToDoDbContextFactory : IDesignTimeDbContextFactory<AlbionToDoDbContext>
{
    public AlbionToDoDbContext CreateDbContext(string[] args = default)
    {
        var builder = new DbContextOptionsBuilder<AlbionToDoDbContext>()
            .UseNpgsql(GetConnectionStringFromConfiguration());

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return new AlbionToDoDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration()
            .GetConnectionString("DefaultConnection");
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"..{Path.DirectorySeparatorChar}AlbionToDo"
                )
            )
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
