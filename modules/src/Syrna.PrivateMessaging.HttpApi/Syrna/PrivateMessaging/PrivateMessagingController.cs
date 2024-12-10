using Syrna.PrivateMessaging.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Syrna.PrivateMessaging
{
    [Area(PrivateMessagingRemoteServiceConsts.ModuleName)]
    public abstract class PrivateMessagingController : AbpController
    {
        protected PrivateMessagingController()
        {
            LocalizationResource = typeof(PrivateMessagingResource);
        }
    }
}
