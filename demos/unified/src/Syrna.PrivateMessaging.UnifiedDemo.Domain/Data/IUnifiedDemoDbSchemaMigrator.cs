using System.Threading.Tasks;

namespace Syrna.PrivateMessaging.UnifiedDemo.Data;

public interface IUnifiedDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
