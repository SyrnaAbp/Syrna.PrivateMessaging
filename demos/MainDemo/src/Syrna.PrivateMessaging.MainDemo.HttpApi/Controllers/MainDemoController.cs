using Syrna.PrivateMessaging.MainDemo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Syrna.PrivateMessaging.MainDemo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MainDemoController : AbpControllerBase
{
    protected MainDemoController()
    {
        LocalizationResource = typeof(MainDemoResource);
    }
}
