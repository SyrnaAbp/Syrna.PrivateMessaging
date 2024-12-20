using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.MainDemo.Blazor.Server;

[DependsOn(
    typeof(MainDemoBlazorModule)
)]
public class MainDemoBlazorServerModule : AbpModule
{

}
