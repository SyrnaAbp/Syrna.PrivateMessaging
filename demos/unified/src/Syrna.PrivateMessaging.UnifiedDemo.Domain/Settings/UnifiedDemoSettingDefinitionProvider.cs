using Syrna.PrivateMessaging.UnifiedDemo.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Syrna.PrivateMessaging.UnifiedDemo.Settings;

public class UnifiedDemoSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AlphaSettings.MySetting1));

        //Gridin son filtre ayarlarını anımsa
        context.Add(new SettingDefinition(UnifiedDemoSettings.RememberGridFilterState, "false", L("DisplayName:Syrna.PrivateMessaging.RememberGridFilterState"), L("Description:Syrna.PrivateMessaging.RememberGridFilterState")));
    }
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UnifiedDemoResource>(name);
    }
}
