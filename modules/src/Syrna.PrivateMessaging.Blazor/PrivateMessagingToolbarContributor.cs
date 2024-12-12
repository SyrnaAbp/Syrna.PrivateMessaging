using System.Threading.Tasks;
using Syrna.PrivateMessaging.Authorization;
using Syrna.PrivateMessaging.Blazor.Components;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace Syrna.PrivateMessaging.Blazor
{
    public class PrivateMessagingToolbarContributor : IToolbarContributor
    {
        public virtual async Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name != StandardToolbars.Main)
            {
                return;
            }

            if (await context.IsGrantedAsync(
                PrivateMessagingPermissions.PrivateMessageNotifications.Default))
            {
                context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(PmNotificationViewComponent)));
            }
        }
    }
}