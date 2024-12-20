using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Syrna.PrivateMessaging.ObjectExtending;

public class PrivateMessagingModuleExtensionConfiguration : ModuleExtensionConfiguration
{
    public PrivateMessagingModuleExtensionConfiguration ConfigurePrivateMessage(
        Action<EntityExtensionConfiguration> configureAction)
    {
        return this.ConfigureEntity(
            PrivateMessagingModuleExtensionConsts.EntityNames.PrivateMessage,
            configureAction
        );
    }
}
