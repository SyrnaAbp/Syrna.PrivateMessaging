using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Syrna.PrivateMessaging.ObjectExtending;

public static class PrivateMessagingModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigurePrivateMessaging(
        this ModuleExtensionConfigurationDictionary modules,
        Action<PrivateMessagingModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            PrivateMessagingModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
