using System.Threading.Tasks;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.Localization;
using Volo.Abp.UI.Navigation;

namespace Syrna.PrivateMessaging.Web.Menus;

public class PrivateMessagingMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<PrivateMessagingResource>();
        //Add main menu items.

        if (await context.IsGrantedAsync(PrivateMessagingPermissions.PrivateMessages.Default))
        {
            context.Menu.GetAdministration().AddItem(new ApplicationMenuItem(PrivateMessagingMenus.Prefix,
                displayName: l["Menu:PrivateMessage"], "~/PrivateMessaging/PrivateMessages/PrivateMessage", icon: "fa fa-messages"));
        }
    }
}