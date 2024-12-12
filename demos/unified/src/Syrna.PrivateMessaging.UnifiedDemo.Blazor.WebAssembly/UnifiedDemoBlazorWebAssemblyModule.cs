using Syrna.PrivateMessaging.UnifiedDemo.Blazor;
using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.UnifiedDemo.Blazor.WebAssembly;

[DependsOn(
    typeof(UnifiedDemoBlazorModule)
)]
public class UnifiedDemoBlazorWebAssemblyModule : AbpModule
{
}
