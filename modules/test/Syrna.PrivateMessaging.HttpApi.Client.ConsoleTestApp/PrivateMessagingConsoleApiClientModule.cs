using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging
{
    [DependsOn(
        typeof(PrivateMessagingHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class PrivateMessagingConsoleApiClientModule : AbpModule
    {
        
    }
}
