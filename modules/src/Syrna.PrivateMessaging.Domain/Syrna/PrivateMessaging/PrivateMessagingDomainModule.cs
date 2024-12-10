using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging
{
    [DependsOn(
        typeof(PrivateMessagingDomainSharedModule)
        )]
    public class PrivateMessagingDomainModule : AbpModule
    {

    }
}
