using Syrna.PrivateMessaging.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Syrna.PrivateMessaging.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class PrivateMessagingPageModel : AbpPageModel
    {
        protected PrivateMessagingPageModel()
        {
            LocalizationResourceType = typeof(PrivateMessagingResource);
            ObjectMapperContext = typeof(PrivateMessagingWebModule);
        }
    }
}