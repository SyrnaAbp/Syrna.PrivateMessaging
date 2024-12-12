using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(PrivateMessagingBlazorModule)
        )]
    public class PrivateMessagingBlazorServerModule : AbpModule
    {
        
    }
}