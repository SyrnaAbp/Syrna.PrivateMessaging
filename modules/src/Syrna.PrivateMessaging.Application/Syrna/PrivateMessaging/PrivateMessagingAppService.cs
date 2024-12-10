using Syrna.PrivateMessaging.Localization;
using Volo.Abp.Application.Services;

namespace Syrna.PrivateMessaging
{
    public abstract class PrivateMessagingAppService : ApplicationService
    {
        protected PrivateMessagingAppService()
        {
            LocalizationResource = typeof(PrivateMessagingResource);
            ObjectMapperContext = typeof(PrivateMessagingApplicationModule);
        }
    }
}
