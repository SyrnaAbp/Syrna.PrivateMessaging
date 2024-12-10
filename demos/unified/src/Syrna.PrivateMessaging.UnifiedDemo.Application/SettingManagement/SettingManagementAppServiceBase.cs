using Syrna.PrivateMessaging.UnifiedDemo;
using Syrna.PrivateMessaging.UnifiedDemo.Localization;
using Volo.Abp.Application.Services;

namespace Syrna.PrivateMessaging.UnifiedDemo.SettingManagement;

public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(UnifiedDemoApplicationModule);
        LocalizationResource = typeof(UnifiedDemoResource);
    }
}
