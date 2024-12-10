using System.IO;
using Syrna.PrivateMessaging.UnifiedDemo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Syrna.PrivateMessaging.UnifiedDemo.SqlServer.EntityFrameworkCore;

/* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
public class UnifiedDemoMigrationsDbContextFactory : IDesignTimeDbContextFactory<UnifiedDemoMigrationsDbContext>
{
    public UnifiedDemoMigrationsDbContext CreateDbContext(string[] args)
    {
        UnifiedDemoEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<UnifiedDemoMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new UnifiedDemoMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Syrna.PrivateMessaging.UnifiedDemo.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
