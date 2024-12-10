using Volo.Abp.Modularity;

namespace Syrna.PrivateMessaging
{
    [DependsOn(
        typeof(PrivateMessagingApplicationModule),
        typeof(PrivateMessagingDomainTestModule)
        )]
    public class PrivateMessagingApplicationTestModule : AbpModule
    {

    }
}
