using Syrna.PrivateMessaging.UnifiedDemo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Syrna.PrivateMessaging.UnifiedDemo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class UnifiedDemoController : AbpControllerBase
{
    protected UnifiedDemoController()
    {
        LocalizationResource = typeof(UnifiedDemoResource);
    }
}
