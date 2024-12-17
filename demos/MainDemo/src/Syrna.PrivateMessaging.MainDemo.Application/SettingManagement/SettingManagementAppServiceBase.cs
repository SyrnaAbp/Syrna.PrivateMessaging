using Syrna.PrivateMessaging.MainDemo;
using Syrna.PrivateMessaging.MainDemo.Localization;
using Volo.Abp.Application.Services;

namespace Syrna.PrivateMessaging.MainDemo.SettingManagement;

public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(MainDemoApplicationModule);
        LocalizationResource = typeof(MainDemoResource);
    }
}
