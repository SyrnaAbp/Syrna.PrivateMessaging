using System.Threading.Tasks;

namespace Syrna.PrivateMessaging.MainDemo.Data;

public interface IMainDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
