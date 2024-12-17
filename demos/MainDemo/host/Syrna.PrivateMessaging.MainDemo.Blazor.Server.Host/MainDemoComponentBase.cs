using Syrna.PrivateMessaging.MainDemo.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Syrna.PrivateMessaging.MainDemo.Blazor.Server.Host
{
    public abstract class MainDemoComponentBase : AbpComponentBase
    {
        protected MainDemoComponentBase()
        {
            LocalizationResource = typeof(MainDemoResource);
        }
    }
}
