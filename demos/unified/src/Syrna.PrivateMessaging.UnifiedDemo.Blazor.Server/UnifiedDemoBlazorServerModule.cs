using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.UnifiedDemo.Blazor.Server;

[DependsOn(
    typeof(UnifiedDemoBlazorModule)
)]
public class UnifiedDemoBlazorServerModule : AbpModule
{

}
