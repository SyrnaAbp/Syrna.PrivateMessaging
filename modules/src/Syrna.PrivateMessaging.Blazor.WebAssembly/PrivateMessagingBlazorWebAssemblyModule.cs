using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.Blazor.WebAssembly
{
    [DependsOn(
        typeof(PrivateMessagingBlazorModule),
        typeof(PrivateMessagingHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class PrivateMessagingBlazorWebAssemblyModule : AbpModule
    {
        
    }
}