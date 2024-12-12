using Syrna.PrivateMessaging.UnifiedDemo.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Syrna.PrivateMessaging.UnifiedDemo.Blazor.Server.Host
{
    public abstract class UnifiedDemoComponentBase : AbpComponentBase
    {
        protected UnifiedDemoComponentBase()
        {
            LocalizationResource = typeof(UnifiedDemoResource);
        }
    }
}
