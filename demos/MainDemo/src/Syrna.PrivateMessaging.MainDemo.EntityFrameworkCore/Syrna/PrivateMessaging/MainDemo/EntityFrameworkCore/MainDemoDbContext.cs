using Syrna.PrivateMessaging.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Syrna.PrivateMessaging.MainDemo.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class MainDemoDbContext(DbContextOptions<MainDemoDbContext> options) : AbpDbContext<MainDemoDbContext>(options), IMainDemoDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePrivateMessaging();
    }
}
