using Syrna.PrivateMessaging.MainDemo.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.MainDemo.SqlServer.EntityFrameworkCore;

[DependsOn(typeof(MainDemoEntityFrameworkCoreModule))]
public class MainDemoEntityFrameworkCoreSqlServerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MainDemoMigrationsDbContext>();
    }
}