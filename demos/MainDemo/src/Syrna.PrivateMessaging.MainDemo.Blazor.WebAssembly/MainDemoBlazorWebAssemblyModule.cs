using Syrna.PrivateMessaging.MainDemo.Blazor;
using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.MainDemo.Blazor.WebAssembly;

[DependsOn(
    typeof(MainDemoBlazorModule)
)]
public class MainDemoBlazorWebAssemblyModule : AbpModule
{
}
